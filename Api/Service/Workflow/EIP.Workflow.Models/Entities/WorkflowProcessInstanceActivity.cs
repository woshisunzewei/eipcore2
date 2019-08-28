using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EIP.Common.Entities.CustomAttributes;

namespace EIP.Workflow.Models.Entities
{
    /// <summary>
    ///     Workflow_ProcessInstance_Activity表实体类
    /// </summary>
    [Serializable]
    [Table("Workflow_ProcessInstance_Activity")]
    [Db("EIP_Workflow")]
    public class WorkflowProcessInstanceActivity 
    {
        /// <summary>
        ///     活动Id
        /// </summary>
        [Key]
        public Guid ActivityId { get; set; }

        /// <summary>
        ///     流程实例Id
        /// </summary>
        public Guid ProcessInstanceId { get; set; }

        /// <summary>
        ///     流程实例Id
        /// </summary>
        public Guid ProcessId { get; set; }

        /// <summary>
        ///     名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     左侧
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        ///     距离顶部
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        ///     类型:start round,end round ,task等
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///     高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        ///     表单Id
        /// </summary>
        public Guid? FormId { get; set; }

        /// <summary>
        ///     意见显示
        /// </summary>
        public short? Opinion { get; set; }

        /// <summary>
        ///     审签类型：无审批意见栏,有审批意见需盖章,有审批意见无需盖章
        /// </summary>
        public short? CommentsBelow { get; set; }

        /// <summary>
        ///     超期提示：提醒,不提醒
        /// </summary>
        public short? TimeoutRemind { get; set; }

        /// <summary>
        ///     归档：归档,不归档
        /// </summary>
        public short? Archive { get; set; }

        /// <summary>
        ///     工时(小时)
        /// </summary>
        public int? WorkTime { get; set; }

        /// <summary>
        ///     提醒类型：邮件
        /// </summary>
        public bool? TimeoutRemindTypeEmail { get; set; }

        /// <summary>
        ///     提醒类型：短信
        /// </summary>
        public bool? TimeoutRemindTypeNote { get; set; }

        /// <summary>
        ///     提醒类型：微信
        /// </summary>
        public bool? TimeoutRemindTypeWx { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///     处理者类型： 所有成员,部门,岗位,工作组,人员,发起者,前一步骤处理者,某一步骤处理者,字段值,发起者领导,发起者分管领导,前一步处理者领导,前一步处理者分管领导,发起者上级部门领导,前一步处理者上级部门领导
        /// </summary>
        public short? ProcessorType { get; set; }

        /// <summary>
        ///     选择范围
        /// </summary>
        public string ProcessorHandler { get; set; }

        /// <summary>
        ///     处理策略： 所有人必须同意,一人同意即可,依据人数比例,独立处理,
        /// </summary>
        public short? HandlingStrategy { get; set; }

        /// <summary>
        ///     策略百分比
        /// </summary>
        public short? HandlingStrategyPercentage { get; set; }

        /// <summary>
        ///     按钮
        /// </summary>
        public string Buttons { get; set; }

        /// <summary>
        ///     步骤提交前事件
        /// </summary>
        public string EventSubmitBefore { get; set; }

        /// <summary>
        ///     步骤提交后事件
        /// </summary>
        public string EventSubmitAfter { get; set; }

        /// <summary>
        ///     步骤退回前事件
        /// </summary>
        public string EventBackBefore { get; set; }

        /// <summary>
        ///     步骤退回后事件
        /// </summary>
        public string EventBackAfter { get; set; }

        /// <summary>
        /// </summary>
        public bool Marked { get; set; }
    }
}