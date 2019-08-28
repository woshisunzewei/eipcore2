using System;
using EIP.Common.Core.Auth;
using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Identity
{
    /// <summary>
    /// 获取
    /// </summary>
    public class SystemOrganizationDataPermissionTreeInput : SearchDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 登录人信息
        /// </summary>
        public PrincipalUser PrincipalUser { get; set; }

        /// <summary>
        /// 数据权限对应Sql
        /// </summary>
        public string DataSql { get; set; }


        public bool HaveSelf { get; set; }


    }
}