using System;
using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Permission
{
    public class SystemMenuButtonEditInput:IdInput
    {
        public Guid? MenuId { get; set; }

        public Guid? MenuButtonId { get; set; }
    }
}