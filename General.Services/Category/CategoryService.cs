using General.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using General.Entities.Category;   //通过引擎的方式
//using General.Entities;
using System.Linq;
using General.Core.Data;

namespace General.Services.Category
{
    public class CategoryService : ICategoryService, IGeneralService   //调用的时候顺序在这里看
    {
        private readonly GeneralDbContext _generalDbContext;

        private IRepository<Entities.Category.Category> _categoryRepository;

        //public CategoryService(GeneralDbContext generalDbContext)
        //{
        //    this._generalDbContext = generalDbContext;

        //}

        public CategoryService(IRepository<Entities.Category.Category> categoryRepository)
        {
            this._categoryRepository = categoryRepository;

        }

        public List<Entities.Category.Category> getAll()
        //public List<Entities.Category> getAll() //通过引擎的方式
        {
            //return _generalDbContext.Categories.ToList();
            return _categoryRepository.Table.ToList();
        }
    
    }
}
