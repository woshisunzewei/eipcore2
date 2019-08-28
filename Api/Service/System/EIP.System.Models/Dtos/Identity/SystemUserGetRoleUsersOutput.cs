using System.Collections.Generic;
using EIP.Common.Entities.Dtos.IView;

namespace EIP.System.Models.Dtos.Identity
{
    public class SystemUserGetRoleUsersOutput
    {
        /// <summary>
        /// 选择框
        /// </summary>
        public IList<TransferDto> AllUser { get; set; }

        /// <summary>
        /// 已有人员
        /// </summary>
        public IList<string> HaveUser { get; set; }
    }
}