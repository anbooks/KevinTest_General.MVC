using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Entities.SysUser;
using General.Framework.Controllers.Admin;
using General.Framework.Security.Admin;
using General.Services.SysUser;
using Microsoft.AspNetCore.Mvc;

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
        private ISysUserService _sysUserService;

        private IAdminAuthService _authenticationService;

        public LoginController(ISysUserService sysUserService,IAdminAuthService authenticationService)
        {
            this._sysUserService = sysUserService;
            this._authenticationService = authenticationService;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("login", Name = "adminLogin")]
        public IActionResult LoginIndex()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LoginIndex(LoginModel model)
        {
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


    }
}