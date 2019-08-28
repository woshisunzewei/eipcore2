namespace EIP.System.Models.Enums
{
    /// <summary>
    /// 枚举字段对齐方式
    /// </summary>
    public enum EnumFieldAlign : byte
    {
        Right,
        Center,
        Left
    }

    /// <summary>
    /// 字段格式化类型
    /// </summary>
    public enum EnumFieldFormatter : byte
    {
        Datetime,
        Icon,
        Sex,
        State,
        YesOrNo,
        UpOrdel,
        Del,
        Update,
        Email
    }

    /// <summary>
    /// 字段排序方式
    /// </summary>
    public enum EnumFieldSortType : byte
    {
        @Int,
        Integer,
        @Float,
        Number,
        Currency,
        Date,
        Text
    }
}