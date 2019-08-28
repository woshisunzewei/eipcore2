using EIP.Common.Entities.Dtos;

namespace EIP.Common.Entities.Paging
{
    /// <summary>
    /// 说  明:分页基础参数
    /// 备  注:用于分页实体继承,必须基础该实体才可进行分页
    /// 编写人:孙泽伟-2015/03/25
    /// </summary>
    public class QueryParam: SearchDto
    {
        /// <summary>
        /// 无参构造函数,提供默认值
        /// </summary>
        public QueryParam()
        {
            Limit = 100;
            Page = 1;
            Sord = "asc";
        }

        /// <summary>
        /// 页码,如:1
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页显示数量,如:100
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// 排序字段(可多个),如:Title
        /// </summary>
        public string Sidx { get; set; }

        /// <summary>
        /// 默认排序方式,如:asc
        /// </summary>
        public string Sord { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public long RecordCount { get; set; }

        /// <summary>
        /// 是否为导出
        /// </summary>
        public bool IsReport { get; set; }

        #region 存储过程分页使用
        /// <summary>
        /// 表名,多表是请使用 tA a inner join tB b On a.AID = b.AID
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        ///主键，可以带表头 a.AID
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// 返回字段默认为*
        /// </summary>
        public string Fields { get; set; }
        #endregion

    }
}