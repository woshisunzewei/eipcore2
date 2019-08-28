using System;

namespace EIP.Common.Entities.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class DbAttribute : Attribute
    {
        public DbAttribute(string name)
        {
            Name = name;
        }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Name { get; set; }
    }
}