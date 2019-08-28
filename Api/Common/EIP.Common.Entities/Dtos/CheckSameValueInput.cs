namespace EIP.Common.Entities.Dtos
{
    /// <summary>
    /// 是否具有相同值输入DTO
    /// </summary>
    public class CheckSameValueInput : NullableIdInput
    {
        /// <summary>
        /// 值
        /// </summary>
        public string Param { get; set; }
    }

    /// <summary>
    /// 传递一个可为空的Id给服务方法
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public class CheckSameValueInput<TId> : IInputDto
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public TId Id { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CheckSameValueInput()
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        public CheckSameValueInput(TId id)
        {
            Id = id;
        }

        /// <summary>
        /// 值
        /// </summary>
        public string Param { get; set; }

    }
}