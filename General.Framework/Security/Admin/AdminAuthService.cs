using General.Entities.SysUser;
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
        public AdminAuthService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;

        }
        public SysUser getCurrentUser()
        {
            return new SysUser() { Id = Guid.NewGuid().ToString(), Name = "李四" };
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


            _httpContextAccessor.HttpContext.SignInAsync("General",claimsPrincipal);
        }

    }
}
