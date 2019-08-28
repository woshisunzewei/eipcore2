using EIP.System.Models.Entities;

namespace EIP.System.Models.Dtos.Permission
{
    public class SystemMenuGetMenuByParentIdOutput:SystemMenu
    {
        /// <summary>
        /// 父级名称
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 父级所有
        /// </summary>
        public string ParentNames { get; set; }
    }
}