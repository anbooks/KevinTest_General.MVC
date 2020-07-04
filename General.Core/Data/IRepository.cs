using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    //数据库的一些基本方法
    public interface IRepository<TEntity> where TEntity :class
    {
        /// <summary>
        /// 
        /// </summary>
        DbContext DbContext { get; }

        /// <summary>
        /// 
        /// </summary>
        DbSet<TEntity> Entities { get; }
        /// <summary>
        /// 查表
        /// </summary>
        IQueryable<TEntity> Table { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //通过主键id获取值
       TEntity getById(object id);
       void insert(TEntity entity, bool isSave = true);
       void update(TEntity entity, bool isSave = true);
       void delete(TEntity entity, bool isSave = true);
    }
}
