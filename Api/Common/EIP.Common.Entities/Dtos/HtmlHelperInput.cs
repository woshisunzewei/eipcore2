using System;

namespace EIP.Common.Entities.Dtos
{
    /// <summary>
    /// 基础HtmlHelperInput
    /// </summary>
    public class BaseHtmlHelperInput : IInputDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 控件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Html描述
        /// </summary>
        public object HtmlAttributes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Placeholder { get; set; }
    }

    /// <summary>
    /// 基础下拉框HtmlHelperInput
    /// </summary>
    public class BaseDropDownListInput : BaseHtmlHelperInput
    {
        /// <summary>
        /// 是否需要默认值
        /// </summary>
        public bool NeedDefault { get; set; }

        /// <summary>
        /// 默认选中值
        /// </summary>
        public dynamic SelectedVal { get; set; }
    }
    
    /// <summary>
    /// 枚举下拉框
    /// </summary>
    public class DropDownListEnumInput : BaseDropDownListInput
    {
        /// <summary>
        /// 枚举类型
        /// </summary>
        public Object EnumType { get; set; }

        /// <summary>
        /// 比较类型
        /// </summary>
        public EnumCompareType CompareType { get; set; }
    }

    /// <summary>
    /// 字典下拉Input
    /// </summary>
    public class DropDownListDictionaryInput : BaseDropDownListInput
    {
        /// <summary>
        /// 字典代码值
        /// </summary>
        public string Code { get; set; }
    }

    /// <summary>
    /// 复选框Input
    /// </summary>
    public class CheckBoxInput : BaseHtmlHelperInput
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsCheck { get; set; }
    }
    
    /// <summary>
    /// 编辑界面顶部提示Input
    /// </summary>
    public class EditTopRemarkInput : BaseHtmlHelperInput
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Remark { get; set; }

    }

    /// <summary>
    /// 消息框输入参数
    /// </summary>
    public class MessageBoxInput : BaseHtmlHelperInput
    {
        /// <summary>
        /// 提示类型
        /// </summary>
        public EnumMessageBoxType MessageBoxType { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
    }
    
    #region 枚举
    /// <summary>
    /// 提醒消息类型
    /// </summary>
    public enum EnumMessageBoxType
    {
        /// <summary>
        /// 错误
        /// </summary>
        error,
        /// <summary>
        /// 提醒
        /// </summary>
        info,
        /// <summary>
        /// 警告
        /// </summary>
        warning,
        /// <summary>
        /// 禁止
        /// </summary>
        forbidden,
        /// <summary>
        /// 暂停
        /// </summary>
        stop,
        /// <summary>
        /// 空白
        /// </summary>
        blank
    }

    /// <summary>
    /// 比较类型
    /// </summary>
    public enum EnumCompareType
    {
        Value,
        Text
    }
    #endregion

    public class BaseBootstrapInput : BaseHtmlHelperInput
    {
        /// <summary>
        /// 是否验证
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// 不允许为空
        /// </summary>
        public string NotemptyMessage { get; set; }

        /// <summary>
        /// 是否远程验证
        /// </summary>
        public bool Remote { get; set; }

        /// <summary>
        /// 远程验证Url
        /// </summary>
        public string RemoteUrl { get; set; }

        /// <summary>
        /// 验证不通过消息
        /// </summary>
        public string RemoteMessage { get; set; }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// 正则表达式消息
        /// </summary>
        public string RegexpMessage { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public bool StringLength { get; set; }

        /// <summary>
        /// 最小长度
        /// </summary>
        public int StringLengthMin { get; set; }

        /// <summary>
        /// 最大长度
        /// </summary>
        public int StringLengthMax { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string StringLengthMessage { get; set; }

        /// <summary>
        /// 清空按钮
        /// </summary>
        public bool ClearButton { get; set; }
    }

    /// <summary>
    /// BootstrapBoxInput文本框
    /// </summary>
    public class BootstrapBoxInput : BaseBootstrapInput
    {
        /// <summary>
        /// 样式
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// /值
        /// </summary>
        public object Value { get; set; }
    }
}