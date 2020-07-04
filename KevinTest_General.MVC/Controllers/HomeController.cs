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
using General.Core.Data;
using General.Entities.Category;
using General.Framework.Controllers;

namespace KevinTest_General.MVC.Controllers
{
    public class HomeController : BaseController  //基础控制类，里面放公共变量啥的
    //public class HomeController : Controller
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


        //private ICategoryService _categoryService;

        //这个不知道从哪里开始？
        //private IRepository<Category> _categoryRepository;
        //private IRepository<SysUser> _sysUserRepository;
        //private IRepository<SysUser> _userRepository;


        //public HomeController(IRepository<Category> categoryRepository,IRepository<SysUser> sysUserRepository, IRepository<SysUser> userRepository)
        //{
        //    this._categoryRepository = categoryRepository;
        //    this._sysUserRepository = sysUserRepository;
        //    this._userRepository = userRepository;

        //}

        private ICategoryService _categoryService;

        public HomeController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }


        public IActionResult Index()
        {
            //测试一下数据库连接
            //var list = _generalDbContext.Categoriess.ToList();


            //var list=  _categoryService.getAll();

            //_categoryService = EnginContext.Current.Resolve<ICategoryService>();
            //var list = _categoryService.getAll();

            //bool b = Object.ReferenceEquals(_categoryRepository.DbContext, _sysUserRepository.DbContext);  //the same
            //bool s = Object.ReferenceEquals(_userRepository, _sysUserRepository);


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
