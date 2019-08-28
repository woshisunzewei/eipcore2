using EIP.Common.Entities.Dtos;
using EIP.System.Models.Enums;

namespace EIP.System.Models.Dtos.Config
{
    /// <summary>
    /// 代码生成器:Pc表单编辑器编辑界面
    /// </summary>
    public class SystemCodeGenerationPcEditDoubleWay : IDoubleWayDto
    {
        /// <summary>
        /// 字段标识
        /// </summary>
        public string ControlId { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string ControlName { get; set; }

        /// <summary>
        /// 字段验证
        /// </summary>
        public EnumControlValidator ControlValidator { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public EnumControlType ControlType { get; set; }

        /// <summary>
        /// 是否合并列
        /// </summary>
        public short ControlColspan { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string ControlDefault { get; set; }
    }
}