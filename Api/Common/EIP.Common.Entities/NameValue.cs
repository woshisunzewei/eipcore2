namespace EIP.Common.Entities
{
    /// <summary>
    ///     名称值对象
    /// </summary>
    public class NameValue<T>
    {
        /// <summary>
        ///     名称
        /// </summary>
        private string _name;

        /// <summary>
        ///     值
        /// </summary>
        private T _value;

        public NameValue()
        {
        }

        public NameValue(string name, T value)
        {
            _name = name;
            _value = value;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}