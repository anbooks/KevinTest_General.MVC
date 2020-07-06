using General.Core.Data;
using General.Core.Librs;
using General.Entities;
using General.Entities.SysUserToken;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General.Services.SysUser
{
    public class SysUserService : ISysUserService, IGeneralService
    {
        private readonly GeneralDbContext _generalDbContext;

        private IMemoryCache _memoryCache;  //缓存类
        private const string MODEL_KEY = "General.services.user_{0}";

        private IRepository<Entities.SysUser.SysUser> _sysUserRepository;


        private IRepository<Entities.SysUserToken.SysUserToken> _sysUserTokenRepository;
        //public CategoryService(GeneralDbContext generalDbContext)
        //{
        //    this._generalDbContext = generalDbContext;

        //}

        public SysUserService(IRepository<Entities.SysUser.SysUser> sysUserRepository, IRepository<Entities.SysUserToken.SysUserToken> sysUserTokenRepository,
            IMemoryCache memoryCache)
        {
            this._sysUserRepository = sysUserRepository;
            this._memoryCache = memoryCache;
            this._sysUserTokenRepository = sysUserTokenRepository;
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

           
            if (!user.Enabled)
                return (false, "你的账号已被冻结", null, null);



            if (user.LoginLock)
            {
                if (user.AllowLoginTime > DateTime.Now)
                {
                    return (false, "账号已被锁定" + ((int)(user.AllowLoginTime - DateTime.Now).Value.TotalMinutes + 1) + "分钟。", null, null);
                }
            }


            //password   =  161e7675716c353c9673322b423acccd   
            //e065630e2e15d080fb6e72e0c1a2144e



            var md5Password =EncryptorHelper.GetMD5(user.Password + r);
            if (password.Equals(md5Password, StringComparison.InvariantCultureIgnoreCase))
            {

                user.LoginLock = false;
                user.LoginFailedNum = 0;
                user.AllowLoginTime = null;
                user.LastLoginTime = DateTime.Now;
                user.LastIpAddress = "";

                //登录日志
                user.SysUserLoginLogs.Add(new Entities.SysUserLoginLog.SysUserLoginLog()
                {
                    Id = Guid.NewGuid(),
                    IpAddress = "",
                    LoginTime = DateTime.Now,
                    Message = "登录：成功"
                });

                //如果需要单点登录，需要溢出下面的旧的登录信息token


                var userToken = new SysUserToken()
                {
                    Id = Guid.NewGuid(),
                    ExpireTime = DateTime.Now.AddDays(15)
                };

                user.SysUserTokens.Add(userToken);
                return (true, "登录成功", userToken.Id.ToString(), user);

            }
            else
            {
                //登录日志
                user.SysUserLoginLogs.Add(new Entities.SysUserLoginLog.SysUserLoginLog()
                {
                    Id = Guid.NewGuid(),
                    IpAddress = "",
                    LoginTime = DateTime.Now,
                    Message = "登录:密码错误"
                });
                user.LoginFailedNum++;
                if (user.LoginFailedNum > 5)
                {
                    user.LoginLock = true;
                    user.AllowLoginTime = DateTime.Now.AddHours(1);  //锁一小时
                }
                _sysUserRepository.DbContext.SaveChanges();
            }

            //return (false, "用户名或密码错误", "aaaa1111", new Entities.SysUser.SysUser() { Id=Guid.NewGuid(),Name="李四"});
            return (false, "用户名或密码错误", null,null);

        }

        /// <summary>
        /// 通过账号获取用户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Entities.SysUser.SysUser getByAccount(string account)
        {
            //return _sysUserRepository.Table.FirstOrDefault(o => o.Account == account);
            return _sysUserRepository.Table.FirstOrDefault(o => o.Account == account && !o.IsDeleted);
        }

        /// <summary>
        /// 通过当前登录用户的token 获取用户信息，并缓存
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Entities.SysUser.SysUser getLogged(string token)
        {
            Entities.SysUserToken.SysUserToken userToken = null;
            Entities.SysUser.SysUser sysUser = null;

            _memoryCache.TryGetValue<Entities.SysUserToken.SysUserToken>(token, out userToken);
            if (userToken != null)
            {
                _memoryCache.TryGetValue(String.Format(MODEL_KEY, userToken.SysUserId), out sysUser);
            }
            if (sysUser != null)
                return sysUser;

            Guid tokenId = Guid.Empty;

            if (Guid.TryParse(token, out tokenId))
            {
                var tokenItem = _sysUserTokenRepository.Table.Include(x => x.SysUser)
                     .FirstOrDefault(o => o.Id == tokenId);
                if (tokenItem != null)
                {
                    _memoryCache.Set(token, tokenItem, DateTimeOffset.Now.AddHours(4));
                    //缓存
                    _memoryCache.Set(String.Format(MODEL_KEY, tokenItem.SysUserId), tokenItem.SysUser, DateTimeOffset.Now.AddHours(4));
                    return tokenItem.SysUser;
                }
            }
            return null;
        }



    }
}
