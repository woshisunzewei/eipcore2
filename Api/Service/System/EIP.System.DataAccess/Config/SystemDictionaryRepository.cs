using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EIP.Common.Dapper;
using EIP.Common.DataAccess;
using EIP.Common.Entities.Dtos;
using EIP.Common.Entities.Tree;
using EIP.System.Models.Dtos.Config;
using EIP.System.Models.Entities;

namespace EIP.System.DataAccess.Config
{
    /// <summary>
    ///     字典数据访问接口实现
    /// </summary>
    public class SystemDictionaryRepository : DapperAsyncRepository<SystemDictionary>, ISystemDictionaryRepository
    {
        /// <summary>
        ///     根据所有字段信息
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<JsTreeEntity>> GetDictionaryTree()
        {
            var sql = new StringBuilder();
            sql.Append("SELECT DictionaryId id,ParentId parent,name text FROM System_Dictionary ORDER BY OrderNo");
            return SqlMapperUtil.SqlWithParams<JsTreeEntity>(sql.ToString());
        }

        /// <summary>
        ///     根据字典代码获取对应下级值
        /// </summary>
        /// <param name="input">代码值</param>
        /// <returns></returns>
        public Task<IEnumerable<SystemDictionaryGetByParentIdOutput>> GetDictionariesParentId(SystemDictionaryGetByParentIdInput input)
        {
            var sql = new StringBuilder();
            sql.Append("select *,(select name from System_Dictionary d where d.DictionaryId=dic.ParentId) ParentName from System_Dictionary dic WHERE 1=1 ");
            if (input.Id != null)
            {
                sql.Append(" and dic.ParentIds  like '%" + (input.Id + ",").Replace(",", @"\,") + "%" + "' escape '\\'");
            }
            sql.Append(input.Sql);
            sql.Append(" ORDER BY dic.OrderNo");
            return SqlMapperUtil.SqlWithParams<SystemDictionaryGetByParentIdOutput>(sql.ToString());
        }

        /// <summary>
        /// 根据ParentIds获取所有下级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<IEnumerable<JsTreeEntity>> GetDictionaryTreeByParentIds(IdInput input)
        {
            var sql = new StringBuilder();
            sql.Append(@"SELECT DictionaryId id,ParentId parent,name text FROM System_Dictionary WHERE ParentIds like '" + (input.Id + ",").Replace(",", @"\,") + "%" + "' escape '\\' OR ParentIds ='" + input.Id + "' ORDER BY OrderNo");
            return SqlMapperUtil.SqlWithParams<JsTreeEntity>(sql.ToString());
        }
    }
}