using System.Collections.Generic;
using EIP.Common.Entities.Dtos.IView;

namespace EIP.System.Models.Dtos.Identity
{
    public class SystemRoleChosenOutput
    {
        /// <summary>
        /// 选择框
        /// </summary>
        public IList<TransferDto> AllRole { get; set; }

        /// <summary>
        /// 已有角色
        /// </summary>
        public IList<string> HaveRole { get; set; }
    }
}