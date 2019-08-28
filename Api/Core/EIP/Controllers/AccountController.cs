using EIP.Common.Core.Auth;
using EIP.Common.Core.Log;
using EIP.Common.Entities;
using EIP.Common.Entities.Dtos;
using EIP.Common.Restful;
using EIP.Common.Restful.Attribute;
using EIP.Common.Restful.Jwt;
using EIP.System.Business.Identity;
using EIP.System.Business.Log;
using EIP.System.Models.Dtos.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace EIP.Controllers
{
    /// <summary>
    /// 帐号控制器
    /// </summary>
    public class AccountController : BaseController
    {
        private readonly IOptions<JwtConfiguration> _jwtConfig;
        private readonly ISystemUserInfoLogic _userInfoLogic;
        private readonly ISystemOrganizationLogic _organizationLogic;
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jwtConfig"></param>
        /// <param name="userInfoLogic"></param>
        /// <param name="organizationLogic"></param>
        /// <param name="accessor"></param>
        /// <param name="loginLogLogic"></param>
        public AccountController(IOptions<JwtConfiguration> jwtConfig,
            ISystemUserInfoLogic userInfoLogic,
            ISystemOrganizationLogic organizationLogic,
            IHttpContextAccessor accessor,
            ISystemLoginLogLogic loginLogLogic)
        {
            _jwtConfig = jwtConfig;
            _userInfoLogic = userInfoLogic;
            _organizationLogic = organizationLogic;
            _accessor = accessor;
           
        }

        #region Login
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [CreateBy("孙泽伟")]
        [Remark("帐号管理-方法-登录")]
        public async Task<JsonResult> Login(SystemUserLoginInput input)
        {
            var operateStatus = new OperateStatus();
            string tokenData = String.Empty;
            //验证数据库信息
            var info = await _userInfoLogic.CheckUserByCodeAndPwd(input);
            if (info.Data != null)
            {
                ICollection<string> roles = new List<string>();
                if (info.Data.IsAdmin)
                {
                    //查询顶级组织机构
                    var orgs = (await _organizationLogic.GetSystemOrganizationByPid(new IdInput(Guid.Empty))).FirstOrDefault();
                    if (orgs != null)
                    {
                        info.Data.OrganizationId = Guid.Parse(orgs.id.ToString());
                        info.Data.OrganizationName = orgs.text;
                    }
                    roles.Add("Admin");
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Value.Secret));
                var header = new JwtHeader(new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
                var issuer = _jwtConfig.Value.Issuer;
                var loginTime = DateTime.Now;
                var claims = new[]
                {
                        new Claim("Name", info.Data.Name),
                        new Claim("Code", info.Data.Code),
                        new Claim("OrganizationId", info.Data.OrganizationId==Guid.Empty?"":info.Data.OrganizationId.ToString()),
                        new Claim("OrganizationName", info.Data.OrganizationName ?? ""),
                        new Claim("LoginId",info.Data.LoginId.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, info.Data.UserId.ToString())
                    }.Concat(roles.Select(role => new Claim("role", role)));
                JwtPayload payload = input.Remberme ? new JwtPayload(issuer, null, claims, null, loginTime.AddYears(1)) : new JwtPayload(issuer, null, claims, null, loginTime.AddMinutes(60));
                var token = new JwtSecurityToken(header, payload);
                operateStatus.ResultSign = ResultSign.Successful;
                tokenData = new JwtSecurityTokenHandler().WriteToken(token);
                WriteLoginLog(info.Data);
            }
            else
            {
                operateStatus.ResultSign = ResultSign.Error;
                operateStatus.Message = info.Message;
            }
            return Json(new
            {
                operateStatus.ResultSign,
                operateStatus.Message,
                Data = tokenData,
                OrganizationId = info.Data?.OrganizationId ?? Guid.Empty,
                UserName = info.Data != null ? info.Data.Name : "",
                OrganizationName = info.Data != null ? info.Data.OrganizationName : "",
                Code = info.Data != null ? info.Data.Code : "",
                HeadImage = info.Data != null ? info.Data.HeadImage : ""
            });
        }
        #endregion

        /// <summary>
        /// 写登录日志
        /// </summary>
        /// <returns></returns> 
        [AllowAnonymous]
        [HttpGet]
        private async void WriteLoginLog(SystemUserLoginOutput input)
        {
            //客户端Ip
            LoginLogHandler handler = new LoginLogHandler(new PrincipalUser()
            {
                Code = input.Code,
                UserId = input.UserId,
                Name = input.Name
            }, _accessor, input.LoginId);
            handler.WriteLog();
        }
    }
}