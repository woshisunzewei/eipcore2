using System;

namespace EIP.System.Models.Dtos.Identity
{
    /// <summary>
    /// 登录输出数据
    /// </summary>
    public class SystemUserLoginOutput
    {
        /// <summary>
        /// 人员Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户登录名
        /// </summary>		
        public string Code { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 组织机构Id
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 冻结
        /// </summary>
        public bool IsFreeze { get; set; }

        /// <summary>
        /// 第一次访问时间
        /// </summary>
        public DateTime? FirstVisitTime { get; set; }

        /// <summary>
        /// 此次登录Id
        /// </summary>
        public Guid LoginId { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImage { get; set; }

        /// <summary>
        /// 是否为超级管理员
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}