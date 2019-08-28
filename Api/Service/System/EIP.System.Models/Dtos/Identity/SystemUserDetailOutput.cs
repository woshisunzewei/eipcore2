using System;
using System.Collections.Generic;
using EIP.Common.Core.Utils;
using EIP.Common.Entities.Dtos;
using EIP.System.Models.Dtos.Permission;
using EIP.System.Models.Enums;

namespace EIP.System.Models.Dtos.Identity
{
    /// <summary>
    /// 用户详细情况Dto
    /// </summary>
    public class SystemUserDetailOutput : IOutputDto
    {
        /// <summary>
        /// 用户登录名
        /// </summary>		
        public string Code { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>		
        public string Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>		
        public string Mobile { get; set; }

        /// <summary>
        /// 第一次访问时间
        /// </summary>		
        public DateTime? FirstVisitTime { get; set; }

        /// <summary>
        /// 最后访问时间
        /// </summary>		
        public DateTime? LastVisitTime { get; set; }

        /// <summary>
        /// 冻结
        /// </summary>
        public bool IsFreeze { get; set; }

        /// <summary>
        /// 格式化冻结
        /// </summary>
        public string IsFreezeFormatter => IsFreeze ? "是" : "否";

        /// <summary>
        /// 备注
        /// </summary>		
        public string Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>		
        public short State { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StateName => EnumUtil.GetEnumNameByIndex<EnumUserNature>(State);

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public IList<SystemPrivilegeDetailListOutput> Role { get; set; }

        /// <summary>
        /// 组
        /// </summary>
        public IList<SystemPrivilegeDetailListOutput> Group { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public IList<SystemPrivilegeDetailListOutput> Post { get; set; }
    }

    
}