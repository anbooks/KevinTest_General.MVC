using General.Core.Data;
using General.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General.Services.SysUser
{
    public class SysUserService : ISysUserService, IGeneralService
    {
        private readonly GeneralDbContext _generalDbContext;

        private IRepository<Entities.SysUser.SysUser> _sysUserRepository;

        //public CategoryService(GeneralDbContext generalDbContext)
        //{
        //    this._generalDbContext = generalDbContext;

        //}

        public SysUserService(IRepository<Entities.SysUser.SysUser> sysUserRepository)
        {
            this._sysUserRepository = sysUserRepository;

        }

        public List<Entities.SysUser.SysUser> getAll()
        //public List<Entities.Category> getAll() //通过引擎的方式
        {
            //return _generalDbContext.Categories.ToList();
            return _sysUserRepository.Table.ToList();
        }

        //------------------------------------------------------

        public (bool, string, string ,Entities.SysUser.SysUser) validateUser(string account, string password, string r)
        {
            //return (false,"密码错误",null,null);
            return (true, "登录成功", "aaaa1111", new Entities.SysUser.SysUser() { Id=Guid.NewGuid().ToString(),Name="李四"});
        }

    }
}
