using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Core.Librs;
using General.Entities.SysUser;
using General.Framework.Controllers.Admin;
using General.Framework.Security.Admin;
using General.Services.SysUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace KevinTest_General.MVC.Areas.Admin.Controllers
{
    /// <summary>
    /// 后台管理登录控制器
    /// </summary>
    //[Route("admin/login")]   //https://localhost:44399/admin/login
    //public class LoginController : AdminAreaController
    //{
    //    //[Route("")]
    //    [Route("",Name="")]
    //    //public IActionResult Index()
    //        public IActionResult LoginIndex()
    //    {
    //        return View();
    //    }
    //}

    //---------------------------------------
    [Route("admin")]   
    public class LoginController : AdminAreaController
    {
        private const string R_KEY = "R_KEY";




        private ISysUserService _sysUserService;

        private IMemoryCache _memoryCache;
        private IAdminAuthService _authenticationService;

        public LoginController(ISysUserService sysUserService,IAdminAuthService authenticationService,
            IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
            this._sysUserService = sysUserService;
            this._authenticationService = authenticationService;
        }




        /// <summary>
        /// 一个客户端代表一个Session 
        /// </summary>
        /// <returns></returns>
        [Route("login", Name = "adminLogin")]
        public IActionResult LoginIndex()
        {
            string r = EncryptorHelper.GetMD5(Guid.NewGuid().ToString());
            HttpContext.Session.SetString(R_KEY, r);     //保存到服务端 这个可以写到页面里面



            LoginModel loginModel = new LoginModel() { R = r };
            return View(loginModel);

           //return View();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LoginIndex(LoginModel model)
        {

            string r = HttpContext.Session.GetString(R_KEY);
            r = r ?? "";   //类似于三木运算符

            if (!ModelState.IsValid)
            {
                AjaxData.Message = "请输入用户账号和密码";
                return Json(AjaxData);
            }


            var result= _sysUserService.validateUser(model.Account, model.Password, "");
            AjaxData.Status = result.Item1;
            AjaxData.Message = result.Item2;

            if (result.Item1)
            {
                //保存登录信息 ClaimsIdentity.ClaimsPrincipal
                _authenticationService.signIn(result.Item3, result.Item4.Name);
            }
            else
            {
                //登录失败
            }

            return Json(AjaxData);
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [Route("getSalt", Name = "getSalt")]
        public IActionResult getSalt(string account)
        {
            var user = _sysUserService.getByAccount(account);
            return Content(user?.Salt);
        }


    }
}