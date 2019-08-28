namespace EIP.Common.Entities.Dtos
{
    /// <summary>
    /// 冻结输入参数
    /// </summary>
    public class FreezeInput : IDto
    {
         /// <summary>
        /// 唯一标识
        /// </summary>
        public bool? IsFreeze { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FreezeInput()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isFreeze"></param>
        public FreezeInput(bool? isFreeze)
        {
            IsFreeze = isFreeze;
        }
    }
}