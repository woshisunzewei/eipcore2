using System;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Enums;

namespace EIP.System.Models.Dtos.Permission
{
    public class SystemPermissionByPrivilegeMasterValueInput:IInputDto
    {
       public  Guid PrivilegeMasterValue { get; set; }
       public EnumPrivilegeMaster PrivilegeMaster { get; set; }
       public EnumPrivilegeAccess PrivilegeAccess { get; set; }
       public Guid? PrivilegeMenuId { get; set; }
    }
}