using System;
using System.ComponentModel.DataAnnotations;
using EIP.Common.Entities.Resx;

namespace EIP.Common.Entities.Dtos
{
    /// <summary>
    /// 以传递一个实体Id值给应用服务方法。
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public class IdInput<TId> : IInputDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required(ErrorMessage = ResourceRequiredErrorMessage.主键不能未空)]
        public TId Id { get; set; }

        public IdInput()
        {
        }

        public IdInput(TId id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// Id类型为Guid的一个快捷实现
    /// </summary>
    public class IdInput : IdInput<Guid>
    {
        public IdInput()
        {

        }

        public IdInput(Guid id)
            : base(id)
        {

        }
    }
}