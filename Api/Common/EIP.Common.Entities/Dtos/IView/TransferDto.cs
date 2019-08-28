namespace EIP.Common.Entities.Dtos.IView
{
    public class TransferDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 显示字
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public  string description { get; set; }

        /// <summary>
        /// 是否可选择
        /// </summary>
        public string disabled { get; set; }
    }
}