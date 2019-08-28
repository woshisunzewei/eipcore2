using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIP.System.Models.Entities
{
    [Serializable]
    [Table("System_DataBase")]
    public class SystemDataBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid DataBaseId { get; set; }

        /// <summary>
        /// 数据库类别:SqlServer/Orcle
        /// </summary>
        public string RdbmsType { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNo { get; set; } = 0;

        /// <summary>
        /// 数据源
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 数据库名
        /// </summary>
        public string CatalogName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
    }
}