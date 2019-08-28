using System.Collections.Generic;
using System.Threading.Tasks;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Permission
{
    public interface ISystemFunctionRepository : IAsyncRepository<SystemFunction>
    {
        /// <summary>
        /// 根据系统代码获取功能项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SystemFunction>> GetSystemFunctionsByAppCode(IdInput<string> input);
    }
}