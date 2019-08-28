using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIP.Common.Business;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.Common.Core.Resource;
using EIP.System.DataAccess.Config;
using EIP.System.Models.Entities;

namespace EIP.System.Business.Config
{
    public class SystemDistrictLogic : DapperAsyncLogic<SystemDistrict>, ISystemDistrictLogic
    {
        #region 构造函数

        private readonly ISystemDistrictRepository _districtRepository;

        public SystemDistrictLogic(ISystemDistrictRepository districtRepository)
            : base(districtRepository)
        {
            _districtRepository = districtRepository;
        }

        #endregion

        #region 方法

        /// <summary>
        ///     根据父级查询所有子集树形结构
        /// </summary>
        /// <param name="input">父级Id</param>
        /// <returns></returns>
        public async Task<IEnumerable<JsTreeEntity>> GetDistrictTreeByParentId(IdInput<string> input)
        {
            var lists = (await _districtRepository.GetDistrictTreeByParentId(input)).ToList();
            foreach (var list in lists)
            {
                input.Id = list.id.ToString();
                var info = (await _districtRepository.GetDistrictTreeByParentId(input)).ToList();
                if (info.Count > 0)
                {
                    list.children = true;
                }
                if (list.parent.ToString() == "0")
                {
                    list.parent = "#";
                }
            }
            return lists;
        }

        /// <summary>
        ///     根据父级查询所有子集
        /// </summary>
        /// <param name="input">父级Id</param>
        /// <returns></returns>
        public async Task<IEnumerable<SystemDistrict>> GetDistrictByParentId(IdInput<string> input)
        {
            return await _districtRepository.GetDistrictByParentId(input);
        }

        /// <summary>
        ///     根据县Id获取省市县Id
        /// </summary>
        /// <param name="input">县Id</param>
        /// <returns></returns>
        public async Task<SystemDistrict> GetDistrictByCountId(IdInput<string> input)
        {
            return await _districtRepository.GetDistrictByCountId(input);
        }

        /// <summary>
        ///     检测代码是否已经具有重复项
        /// </summary>
        /// <param name="input">需要验证的参数</param>
        /// <returns></returns>
        public async Task<OperateStatus> CheckDistrictId(CheckSameValueInput input)
        {
            var operateStatus = new OperateStatus();
            if (await _districtRepository.CheckDistrictId(input))
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = string.Format(Chs.HaveCode, input.Param);
            }
            else
            {
                operateStatus.ResultSign = ResultSign.Successful;
                operateStatus.Message = Chs.CheckSuccessful;
            }
            return operateStatus;
        }

        /// <summary>
        ///     保存省市县信息
        /// </summary>
        /// <param name="systemDistrict">省市县信息</param>
        /// <returns></returns>
        public async Task<OperateStatus> SaveDistrict(SystemDistrict systemDistrict)
        {
            //判断是否具有省市区县信息
            var district = await GetByIdAsync(systemDistrict.DistrictId);
            //有则更新,无则添加
            var operateStatus = district != null ? await UpdateAsync(systemDistrict) : await InsertAsync(systemDistrict);
            return operateStatus;
        }

        /// <summary>
        ///     删除省市县及下级数据
        /// </summary>
        /// <param name="input">父级id</param>
        /// <returns></returns>
        public async Task<OperateStatus> DeleteDistrict(IdInput<string> input)
        {
            var operateStatus = new OperateStatus();
            DeletIds.Add(input.Id);
            await GetDeleteGuid(input);
            foreach (var delete in DeletIds)
            {
                operateStatus = await DeleteAsync(new SystemDistrict()
                {
                    DistrictId = delete
                });
            }
            return operateStatus;
        }

        /// <summary>
        ///     删除主键集合
        /// </summary>
        public IList<string> DeletIds = new List<string>();

        /// <summary>
        ///     获取删除主键信息
        /// </summary>
        /// <param name="input"></param>
        private async Task GetDeleteGuid(IdInput<string> input)
        {
            //获取下级
            var dictionary = (await GetDistrictByParentId(input)).ToList();
            foreach (var dic in dictionary)
            {
                DeletIds.Add(dic.DistrictId);
                await GetDeleteGuid(input);
            }
        }
        #endregion
    }
}