using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Framework.Controllers.Admin;
using Microsoft.AspNetCore.Mvc;

namespace KevinTest_General.MVC.Areas.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginController : AdminAreaController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}