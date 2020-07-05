using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Framework.Controllers.Admin;
using Microsoft.AspNetCore.Mvc;

namespace KevinTest_General.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    // public class HomeController : Controllers
    // public class MainController : AdminAreaController
    public class MainController : PublicAdminController   //登录后才可以查看
    {

        //https://localhost:44399/admin/main/index
        public IActionResult Index()
        {
            var user = WorkContext.CurrentUser;
            return View();
        }
    }
}