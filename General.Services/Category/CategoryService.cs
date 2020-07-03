using General.Entities;
using System;
using System.Collections.Generic;
using System.Text;
//using General.Entities.Category;   通过引擎的方式
using General.Entities;
using System.Linq;

namespace General.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly GeneralDbContext _generalDbContext;

        public CategoryService(GeneralDbContext generalDbContext)
        {
            this._generalDbContext = generalDbContext;

        }

        //public List<Entities.Category.Category> getAll()
        public List<Entities.Category> getAll() //通过引擎的方式
        {
          return _generalDbContext.Categories.ToList();
        }
    
    }
}
