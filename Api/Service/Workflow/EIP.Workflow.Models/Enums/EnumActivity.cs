namespace EIP.Workflow.Models.Enums
{
    /// <summary>
    ///     审批节点意见枚举:显示,不显示等
    /// </summary>
    public enum EnumActivityOpinion
    {
        显示,
        不显示
    }

    /// <summary>
    ///     审批节点超时提醒枚举: 提醒,不提醒等
    /// </summary>
    public enum EnumActivityTimeoutRemind
    {
        提醒,
        不提醒
    }

    /// <summary>
    ///     审批节点超时提醒方式枚举: 邮件,短信,微信等
    /// </summary>
    public enum EnumActivityTimeoutRemindType
    {
        邮件 = 2,
        短信 = 4,
        微信 = 6
    }

    /// <summary>
    ///     审批节点归档枚举:归档,不归档等
    /// </summary>
    public enum EnumActivityArchive
    {
        归档,
        不归档
    }

    /// <summary>
    ///     审批节点意见栏枚举:无审批意见栏,有审批意见需盖章,有审批意见无需盖章等
    /// </summary>
    public enum EnumActivityCommentsBelow
    {
        无审批意见栏 = 2,
        有审批意见需盖章 = 4,
        有审批意见无需盖章 = 6
    }

    /// <summary>
    ///     审批节点-策略-处理者类型:
    /// </summary>
    public enum EnumActivityProcessorType
    {
        所有成员 = 2,
        部门 = 4,
        岗位 = 6,
        工作组 = 8,
        人员 = 10,
        发起者 = 12,
        前一步骤处理者 = 14,
        某一步骤处理者 = 16,
        字段值 = 18,
        发起者领导 = 20,
        发起者分管领导 = 22,
        前一步处理者领导 = 24,
        前一步处理者分管领导 = 26,
        发起者上级部门领导 = 28,
        前一步处理者上级部门领导 = 30
    }

    /// <summary>
    ///     审批节点-策略-处理策略
    /// </summary>
    public enum EnumActivityHandlingStrategy
    {
        所有人必须同意 = 2,
        一人同意即可 = 4,
        依据人数比例 = 6,
        独立处理 = 8
    }

    /// <summary>
    /// 活动状态
    /// </summary>
    public enum EnumActivityStatus
    {
        等待=2,
        正在处理=4,
        完成=6,
        暂停=8,
        撤销=10,
        打回=12,
        取消=14
    }

    /// <summary>
    /// 活动类型
    /// </summary>
    public enum EnumAcitvityType
    {
        开始,
        审批,
        子流程,
        结束
    }
}