using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KevinTest_General.MVC.Models;
using General.Entities;
using General.Services.Category;
using General.Core;

namespace KevinTest_General.MVC.Controllers
{
    public class HomeController : Controller
    {
        //测试一下数据库连接
        //private GeneralDbContext _generalDbContext;
        //public HomeController(GeneralDbContext generalDbContext)
        //{
        //    this._generalDbContext = generalDbContext;
        //}


        //访问category表的方式
        //和上面的注释不一样
        //private ICategoryService _categoryService;

        //public HomeController(ICategoryService categoryService)
        //{
        //    this._categoryService = categoryService;
        //}


        private ICategoryService _categoryService;

        public IActionResult Index()
        {
            //测试一下数据库连接
            //var list = _generalDbContext.Categoriess.ToList();


            //var list=  _categoryService.getAll();

            _categoryService = EnginContext.Current.Resolve<ICategoryService>();
            var list = _categoryService.getAll();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
