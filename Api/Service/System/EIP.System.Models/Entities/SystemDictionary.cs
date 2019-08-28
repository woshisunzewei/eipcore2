using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EIP.Common.Entities.CustomAttributes;
using MongoDB.Bson.Serialization.Attributes;

namespace EIP.System.Models.Entities
{
    /// <summary>
    /// System_Dictionary表实体类
    /// </summary>
    [BsonIgnoreExtraElements]
    [Serializable]
    [Table("System_Dictionary")]
    [Db("EIP")]
    public class SystemDictionary
    {
        /// <summary>
        /// 主键
        /// </summary>		
        [Key]
        public Guid DictionaryId { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>		
        public Guid ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>		
        public string Value { get; set; }

        /// <summary>
        /// 是否允许删除(系统默认配置字段不允许删除)
        /// </summary>		
        public bool CanbeDelete { get; set; }

        /// <summary>
        ///是否冻结
        /// </summary>		
        public bool IsFreeze { get; set; }

        /// <summary>
        /// 排序
        /// </summary>		
		public int OrderNo { get; set; } = 0;

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建用户Id
        /// </summary>		
        public Guid CreateUserId { get; set; }

        /// <summary>
        /// 创建人员名称
        /// </summary>
        public string CreateUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 创建用户Id
        /// </summary>		
        public Guid? UpdateUserId { get; set; }

        /// <summary>
        /// 修改人员名称
        /// </summary>
        public string UpdateUserName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>		
		public string Remark { get; set; }

        /// <summary>
        /// 上级所有字符串方便查询
        /// </summary>		
        public string ParentIds { get; set; }
    }
}
