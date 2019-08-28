using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace EIP.System.Models.Entities
{
    [Serializable]
    [Table( "System_MenuButtonFunction")]
    public class SystemMenuButtonFunction
    {
        /// <summary>
        /// 菜单按钮Id
        /// </summary>
        public Guid MenuButtonId { get; set; }

        /// <summary>
        /// 功能项Id
        /// </summary>
        public Guid FunctionId { get; set; }
    }
}