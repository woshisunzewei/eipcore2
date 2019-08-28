namespace EIP.Workflow.Models.Enums
{
    /// <summary>
    /// 流程状态
    /// </summary>
    public enum EnumProcessStatu
    {
        处理中 = 2,
        已完成 = 4,
        暂停中 = 6
    }

    /// <summary>
    /// 等级
    /// </summary>
    public enum EnumProcessUrgency
    {
        正常 = 2,
        重要 = 4,
        紧急 = 6
    }
}