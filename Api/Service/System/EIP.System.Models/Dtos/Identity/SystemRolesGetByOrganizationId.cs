using System;
using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Identity
{
    public class SystemRolesGetByOrganizationId : SearchDto
    {
        public Guid? Id { get; set; }
    }
}