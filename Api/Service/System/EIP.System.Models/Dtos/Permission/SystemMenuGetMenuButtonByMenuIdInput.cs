using System;
using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Permission
{
    public class SystemMenuGetMenuButtonByMenuIdInput : SearchDto
    {
        public Guid? Id { get; set; }

    }
}