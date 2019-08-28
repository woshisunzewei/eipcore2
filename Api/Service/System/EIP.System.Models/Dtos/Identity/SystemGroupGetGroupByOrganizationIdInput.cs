using System;
using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Identity
{
    public class SystemGroupGetGroupByOrganizationIdInput : SearchDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }

    }
}