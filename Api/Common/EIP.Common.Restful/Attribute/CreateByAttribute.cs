using Microsoft.AspNetCore.Mvc.Filters;
namespace EIP.Common.Restful.Attribute
{
    public class CreateByAttribute : System.Attribute, IFilterMetadata
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">开发人员编码</param>
        public CreateByAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">开发人员编码</param>
        /// <param name="time">开发时间</param>
        public CreateByAttribute(string name, string time)
        {
            Name = name;
            Time = time;
        }

        /// <summary>
        /// 开发人员编码
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 开发时间
        /// </summary>
        public string Time { get; set; }
    }
}