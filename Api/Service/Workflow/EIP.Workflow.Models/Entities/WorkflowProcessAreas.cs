using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EIP.Common.Entities.CustomAttributes;

namespace EIP.Workflow.Models.Entities
{
    /// <summary>
    ///     Workflow_ProcessAreas表实体类
    /// </summary>
    [Serializable]
    [Table("Workflow_ProcessAreas")]
    [Db("EIP_Workflow")]
    public class WorkflowProcessAreas 
    {
        /// <summary>
        ///     区块Id
        /// </summary>
        [Key]
        public string AreasId { get; set; }

        /// <summary>
        ///     流程Id
        /// </summary>
        public Guid ProcessId { get; set; }

        /// <summary>
        ///     区块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     左侧
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        ///     头
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        ///     颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        ///     宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///     高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// </summary>
        public bool Alt { get; set; }
    }
}