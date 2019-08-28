namespace EIP.Common.Entities.Dtos
{
    /// <summary>
    /// Input值
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class Input<TValue> : IInputDto
    {
        public TValue Value { get; set; }

        public Input()
        {
        }

        public Input(TValue value)
        {
            Value = value;
        }
    }

    /// <summary>
    /// Id类型为Guid的一个快捷实现
    /// </summary>
    public class Input : Input<string>
    {
        public Input()
        {

        }
        public Input(string id)
            : base(id)
        {

        }
    }
}