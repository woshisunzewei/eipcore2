using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EIP.System.Models.Enums;

namespace EIP.System.Models.Entities
{
    /// <summary>
    /// System_EmailAccount表实体类
    /// </summary>
    [Serializable]
    [Table("System_EmailAccount")]
    public class SystemEmailAccount 
    {
        /// <summary>
        /// 邮件账号Id
        /// </summary>		
        [Key]
        public Guid EmailAccountId { get; set; }

        /// <summary>
        /// 邮件用户名
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 邮件密码
        /// </summary>		
        public string Password { get; set; }

        /// <summary>
        /// 类型:0错误使用,1其他使用
        /// </summary>		
        public short Type { get; set; }

        /// <summary>
        /// 启用Ssl
        /// </summary>		
        public bool Ssl { get; set; }

        /// <summary>
        /// 端口
        /// </summary>		
        public short? Port { get; set; }

        /// <summary>
        /// Smtp服务器地址:smtp.qq.com
        /// </summary>		
        public string Smtp { get; set; }

        /// <summary>
        /// 是否冻结
        /// </summary>		
        public bool IsFreeze { get; set; }

        /// <summary>
        /// 排序
        /// </summary>		
        public int OrderNo { get; set; }=0;

        /// <summary>
        /// 备注
        /// </summary>		
        public string Remark { get; set; }
        
    }
}
