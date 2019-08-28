using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Permission
{
    /// <summary>
    /// 数据权限规则Json输出参数
    /// 1、不同系统需要的规则参数不一样,可添加此实体进行扩展
    /// </summary>
    public class SystemDataRuleJsonDoubleWay : IDoubleWayDto
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public object Type { get; set; }
    }
}