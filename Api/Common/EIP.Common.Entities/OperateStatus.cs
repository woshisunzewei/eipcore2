using EIP.Common.Core.Resource;

namespace EIP.Common.Entities
{
    /// <summary>
    ///  调用服务或业务逻辑的返回标识枚举,使用DataContract特性,表示可序列化
    /// </summary>
    public enum ResultSign
    {
        /// <summary>
        ///     操作成功
        /// </summary>
        Successful = 0,

        /// <summary>
        ///     警告
        /// </summary>
        Warning = 1,

        /// <summary>
        ///     操作引发错误
        /// </summary>
        Error = 2
    }

    /// <summary>
    ///     调用调用服务或业务逻辑的操作状态,使用DataContract特性,表示可序列化
    /// </summary>
    public class OperateStatus
    {
        #region 构造函数

        /// <summary>
        ///     构造函数:默认为失败
        /// </summary>
        public OperateStatus()
        {
            ResultSign = ResultSign.Error;
            Message = Chs.Error;
        }

        public OperateStatus(OperateStatus status)
        {
            ResultSign = status.ResultSign;
            Message = status.Message;
            
        }

        #endregion

        #region 属性

        /// <summary>
        ///     返回标记
        /// </summary>
        public ResultSign ResultSign { get; set; }

        /// <summary>
        ///     消息字符串(有多语言后将删除该属性)
        /// </summary>
        public object Message { get; set; }
        #endregion
    }

    /// <summary>
    /// 返回结果带实体信息
    /// </summary>
    /// <typeparam name="T">实体信息</typeparam>
    public class OperateStatus<T> : OperateStatus
    {
        public T Data { get; set; }
    }

    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperateType
    {
        新增,
        编辑,
        删除,
        查询
    }
}