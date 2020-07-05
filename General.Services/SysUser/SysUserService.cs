using General.Core.Data;
using General.Core.Librs;
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

        public (bool Status, string Message, string Token, Entities.SysUser.SysUser User) validateUser(string account, string password, string r)
        {
            //return (false,"密码错误",null,null);
            var user = getByAccount(account);
            if (user == null)
                return (false,"用户名或密码错误",null,null);

            var md5Password = EncrytorHelper.GetMD5(user.Password + r);
            if (password.Equals(md5Password, StringComparison.InvariantCultureIgnoreCase))
            {

            };

            return (true, "登录成功", "aaaa1111", new Entities.SysUser.SysUser() { Id=Guid.NewGuid().ToString(),Name="李四"});
        }

        /// <summary>
        /// 通过账号获取用户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Entities.SysUser.SysUser getByAccount(string account)
        {
            return _sysUserRepository.Table.FirstOrDefault(o => o.Account == account);
        }


    }
}
