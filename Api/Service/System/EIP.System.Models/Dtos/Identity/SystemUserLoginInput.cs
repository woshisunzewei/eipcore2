using System.ComponentModel.DataAnnotations;
using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Identity
{
    /// <summary>
    /// 用户登录输入实体
    /// </summary>
    public class SystemUserLoginInput : IInputDto
    {
        /// <summary>
        /// 代码
        /// </summary>
        [Required(ErrorMessage = "代码不能为空")]
        public string Code { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Pwd { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Verify { get; set; }

        /// <summary>
        /// 记住我
        /// </summary>
        public bool Remberme { get; set; }
    }
}