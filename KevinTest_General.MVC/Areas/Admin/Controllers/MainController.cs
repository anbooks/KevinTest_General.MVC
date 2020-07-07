using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Framework.Controllers.Admin;
using General.Framework.Security.Admin;
using Microsoft.AspNetCore.Mvc;

namespace KevinTest_General.MVC.Areas.Admin.Controllers
{
    //    /// <summary>
    ///// ------------------------------------
    ///// </summary>
    //    [Area("Admin")]
    //    // public class HomeController : Controllers
    //    // public class MainController : AdminAreaController
    //    public class MainController : PublicAdminController   //登录后才可以查看
    //    {

    //        //https://localhost:44399/admin/main/index
    //        public IActionResult Index()
    //        {
    //            var user = WorkContext.CurrentUser;
    //            return View();
    //        }
    //    }
    //    //-------------------------------------------
    [Route("admin/main")]
    public class MainController : PublicAdminController
    {
        private IAdminAuthService _adminAuthService;

        public MainController(IAdminAuthService adminAuthService)
        {
            this._adminAuthService = adminAuthService;
        }

        [Route("", Name = "mainIndex")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("out", Name = "signOut")]
        public IActionResult SignOut()
        {
            _adminAuthService.signOut();
            return RedirectToRoute("adminLogin");  //跳转到退出系统
        }

    }

}