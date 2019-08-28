using System;
using EIP.Common.Entities.CustomAttributes;

namespace EIP.Workflow.Models.Dtos
{
    /// <summary>
    /// 流程实例输出
    /// </summary>
    [Db("EIP_Workflow")]
    public class WorkflowProcessGetOutput
    {
        /// <summary>
        /// 实例Id
        /// </summary>
        public Guid ProcessId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>		
        public string Version { get; set; }

        /// <summary>
        /// 状态
        /// </summary>		
        public byte Status { get; set; }

        /// <summary>
        /// 流程类型
        /// </summary>		
        public Guid Type { get; set; }

        /// <summary>
        /// 流程类型
        /// </summary>		
        public string TypeName { get; set; }

        /// <summary>
        /// 设计Json
        /// </summary>		
        public string DesignJson { get; set; }

        /// <summary>
        /// 备注
        /// </summary>		
        public string Remark { get; set; }

        /// <summary>
        /// 冻结
        /// </summary>		
        public bool IsFreeze { get; set; }

        /// <summary>
        /// 排序
        /// </summary>		
        public int OrderNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人员Id
        /// </summary>		
        public Guid CreateUserId { get; set; }

        /// <summary>
        /// 创建人员名称
        /// </summary>		
        public string CreateUserName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>		
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新人员id
        /// </summary>		
        public Guid UpdateUserId { get; set; }

        /// <summary>
        /// 更新人员名称
        /// </summary>		
        public string UpdateUserName { get; set; }
    }
}