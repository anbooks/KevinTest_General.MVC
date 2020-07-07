using General.Entities;
using System;
using System.Collections.Generic;
using System.Text;

//namespace General.Services.Category
namespace General.Services.Category
{
    public  interface ICategoryService
    {
        /// <summary>
        /// 初始化保存方法
        /// </summary>
        /// <param name="list"></param>
        void initCategory(List<Entities.Category> list);
        List<Entities.Category> getAll();
       // List<Entities.Category> getAll();
    }

    public interface IGeneralService
    {

    }
}
