using EIP.System.Models.Entities;

namespace EIP.System.Models.Dtos.Config
{
    /// <summary>
    /// 根据父级id获取字典
    /// </summary>
    public class SystemDictionaryGetByParentIdOutput: SystemDictionary
    {
        /// <summary>
        /// 父级名称
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 所有父级
        /// </summary>
        public string ParentNames { get; set; }
    }
}