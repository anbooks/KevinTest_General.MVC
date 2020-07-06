using General.Entities.SysUser;
using General.Services.SysUser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace General.Framework.Security.Admin
{
    public class AdminAuthService:IAdminAuthService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ISysUserService _sysUserService;
        public AdminAuthService(IHttpContextAccessor httpContextAccessor, ISysUserService sysUserService)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._sysUserService = sysUserService;

        }
        public SysUser getCurrentUser()
        {
            //return new SysUser() { Id = Guid.NewGuid(), Name = "李四" };

            var result = _httpContextAccessor.HttpContext.AuthenticateAsync(CookieAdminAuthInfo.AuthenticationScheme).Result;
            if (result.Principal == null)
                return null;
            //var token = result.Principal.FindFirstValue(ClaimTypes.Sid); //临时改成这个样子的
            var token = result.Principal.FindFirst(ClaimTypes.Sid).Value;
            return _sysUserService.getLogged(token ?? "");
        }

        /// <summary>
        /// 保存登录状态
        /// </summary>
        /// <param name="token"></param>
        /// <param name="name"></param>
        public void signIn(string token, string name)
        {
            //throw new NotImplementedException();
            ClaimsIdentity claimsIdentity = new ClaimsIdentity("Forms");
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid,token));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name,name));
            ClaimsPrincipal claimsPrincipal=new ClaimsPrincipal();


            //_httpContextAccessor.HttpContext.SignInAsync("General",claimsPrincipal);
            _httpContextAccessor.HttpContext.SignInAsync(CookieAdminAuthInfo.AuthenticationScheme, claimsPrincipal);
        }

    }
}
