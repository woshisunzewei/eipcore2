using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EIP.Common.Entities.CustomAttributes;

namespace EIP.Workflow.Models.Entities
{
    /// <summary>
    /// Workflow_Comment表实体类
    /// </summary>
	[Serializable]
    [Table("Workflow_Comment")]
	[Db("EIP_Workflow")]
    public class WorkflowComment
    {
        /// <summary>
        /// 评论
        /// </summary>		
		[Key]
        public Guid CommentId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>		
        public string Content { get; set; }

        /// <summary>
        /// 类型:0管理员添加、1员工添加
        /// </summary>		
        public int Type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>		
        public int OrderNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建者Id
        /// </summary>		
        public Guid? CreateUserId { get; set; }

        /// <summary>
        /// 创建人员名字
        /// </summary>		
        public string CreateUserName { get; set; }

        /// <summary>
        /// 最有一次修改人员时间
        /// </summary>		
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 最后一次修改人员Id
        /// </summary>		
        public Guid? UpdateUserId { get; set; }

        /// <summary>
        /// 最后修改人员
        /// </summary>		
        public string UpdateUserName { get; set; }
    }
}
