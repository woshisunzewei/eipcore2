using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIP.System.Models.Entities
{
    /// <summary>
    /// System_DataLog表实体类
    /// </summary>

    [Table("System_DataLog")]
   public class SystemDataLog 
    {
        /// <summary>
        /// 主键Id
        /// </summary>		
        [Key]
        public Guid DataLogId { get; set; }

        /// <summary>
        ///     操作类型:0新增/2编辑/3删除
        /// </summary>
        public byte OperateType { get; set; }

        /// <summary>
        ///     操作表
        /// </summary>
        public string OperateTable { get; set; }

        /// <summary>
        ///     操作前数据:若为新增，删除等数据
        /// </summary>
        public string OperateData { get; set; }

        /// <summary>
        ///     操作后数据:编辑操作
        /// </summary>
        public string OperateAfterData { get; set; }

        /// <summary>
        ///     创建人员
        /// </summary>
        public Guid CreateUserId { get; set; }

        /// <summary>
        ///     创建人员登录代码
        /// </summary>
        public string CreateUserCode { get; set; }

        /// <summary>
        ///     创建人员名字
        /// </summary>
        public string CreateUserName { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}