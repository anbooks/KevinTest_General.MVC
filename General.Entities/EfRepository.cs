using General.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General.Entities
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private GeneralDbContext _dbContext;

        public EfRepository(GeneralDbContext generalDbContext)
        {
            this._dbContext = generalDbContext;
        }

        public DbContext DbContext
        {
            get
            {
            return _dbContext;
            }
        }

        public DbSet<TEntity> Entities
        {
            get
            {
                return _dbContext.Set<TEntity>();
            }

        }

        /// <summary>
        /// 只是查询使用
        /// </summary>
        public IQueryable<TEntity> Table
        {
            get
            {
                return Entities;
            }
        }


        public void delete(TEntity entity, bool isSave = true)
        {
            //throw new NotImplementedException();
            Entities.Remove(entity);
            if (isSave)
                _dbContext.SaveChanges();
        }

        public TEntity getById(object id)
        {
            return _dbContext.Set<TEntity>().Find(id);
           // throw new NotImplementedException();
        }

        public void insert(TEntity entity, bool isSave = true)
        {
            //throw new NotImplementedException();
            Entities.Add(entity);
            if (isSave)
                _dbContext.SaveChanges();
        }

        public void update(TEntity entity, bool isSave = true)
        {
            // throw new NotImplementedException();
            if (isSave)
                _dbContext.SaveChanges();
        }
    }
}
