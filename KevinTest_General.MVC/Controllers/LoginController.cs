using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KevinTest_General.MVC.Controllers
{
    [Route("login")]   //测试与admin中的重名会有问题
    public class LoginController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}