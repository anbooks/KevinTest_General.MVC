using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Framework.Controllers.Admin;
using Microsoft.AspNetCore.Mvc;

namespace KevinTest_General.MVC.Areas.Admin.Controllers
{
    /// <summary>
    /// 后台管理登录控制器
    /// </summary>
    [Route("admin/login")]   //https://localhost:44399/admin/login
    public class LoginController : AdminAreaController
    {
        //[Route("")]
        [Route("",Name="")]
        //public IActionResult Index()
            public IActionResult LoginIndex()
        {
            return View();
        }
    }
}