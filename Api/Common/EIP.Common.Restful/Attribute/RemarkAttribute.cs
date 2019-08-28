using Microsoft.AspNetCore.Mvc.Filters;

namespace EIP.Common.Restful.Attribute
{
    /// <summary>
    /// 备注
    /// </summary>
    public class RemarkAttribute : System.Attribute, IFilterMetadata
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="describe">内容</param>

        public RemarkAttribute(string describe)
        {
            Describe = describe;
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Describe { get; set; }

    }
}