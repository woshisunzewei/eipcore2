namespace EIP.Workflow.Models.Enums
{
    /// <summary>
    /// 连线类型
    /// </summary>
    public enum EnumLineType
    {
        节点连线 = 2,
        条件连线 = 4,
        退回连线 = 6
    }

    /// <summary>
    /// 退回连线-策略-退回策略:
    /// </summary>
    public enum EnumLineReturnPolicy
    {
        根据处理策略退回 = 2,
        一人退回全部退回 = 4,
        所人有退回才退回 = 6,
        不能退回 = 8
    }
}