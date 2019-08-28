using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EIP.Common.Entities.CustomAttributes;

namespace EIP.Workflow.Models.Entities
{
    /// <summary>
    ///     Workflow_ProcessLine表实体类
    /// </summary>
    [Serializable]
    [Table("Workflow_ProcessLine")]
    [Db("EIP_Workflow")]
    public class WorkflowProcessLine 
    {
        /// <summary>
        ///     连线Id
        /// </summary>
        [Key]
        public Guid LineId { get; set; }

        /// <summary>
        ///     流程Id
        /// </summary>
        public Guid ProcessId { get; set; }

        /// <summary>
        ///     姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     类型:连线类型,取值有”sl”直线,”lr”中段可左右移动的折线,”tb”中段可上下移动的折线
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     当type=”lr”时,为中段的相对于工作区的X坐标值,当type=”tb”时,为中段的相对于工作区的Y坐标值.
        /// </summary>
        public decimal? M { get; set; }

        /// <summary>
        ///     连线的开始节点ID
        /// </summary>
        public Guid From { get; set; }

        /// <summary>
        ///     连线的结束节点ID
        /// </summary>
        public Guid To { get; set; }

        /// <summary>
        ///     连线类型:节点连线,条件连线,退回连线
        /// </summary>
        public byte LineType { get; set; }

        /// <summary>
        ///     退回策略:根据处理策略退回,一人退回全部退回,所人有退回才退回,不能退回
        /// </summary>
        public byte? ReturnPolicy { get; set; }

        /// <summary>
        ///     条件
        /// </summary>
        public string Conditions { get; set; }
    }
}