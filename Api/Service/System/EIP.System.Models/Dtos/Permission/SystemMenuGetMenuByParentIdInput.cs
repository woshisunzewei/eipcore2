using System;
using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Permission
{
    public class SystemMenuGetMenuByParentIdInput:SearchDto
    {
        public Guid? Id { get; set; }
    }
}