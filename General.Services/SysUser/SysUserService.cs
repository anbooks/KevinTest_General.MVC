﻿using General.Core;
using General.Core.Data;
using General.Core.Librs;
using General.Entities;
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

        private IRepository<Entities.SysUser> _sysUserRepository;


        private IRepository<Entities.SysUserToken> _sysUserTokenRepository;
        //public CategoryService(GeneralDbContext generalDbContext)
        //{
        //    this._generalDbContext = generalDbContext;

        //}

        public SysUserService(IRepository<Entities.SysUser> sysUserRepository, IRepository<Entities.SysUserToken> sysUserTokenRepository,
            IMemoryCache memoryCache)
        {
            this._sysUserRepository = sysUserRepository;
            this._memoryCache = memoryCache;
            this._sysUserTokenRepository = sysUserTokenRepository;
        }


        public List<Entities.SysUser> getAll()
        //public List<Entities.Category> getAll() //通过引擎的方式
        {
            //return _generalDbContext.Categories.ToList();
            return _sysUserRepository.Table.ToList();
        }

        //------------------------------------------------------

        public (bool Status, string Message, string Token, Entities.SysUser User) validateUser(string account, string password, string r)
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
                user.SysUserLoginLogs.Add(new Entities.SysUserLoginLog()
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
                user.SysUserLoginLogs.Add(new Entities.SysUserLoginLog()
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
        public Entities.SysUser getByAccount(string account)
        {
            //return _sysUserRepository.Table.FirstOrDefault(o => o.Account == account);
            return _sysUserRepository.Table.FirstOrDefault(o => o.Account == account && !o.IsDeleted);
        }

        /// <summary>
        /// 通过当前登录用户的token 获取用户信息，并缓存
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Entities.SysUser getLogged(string token)
        {
            Entities.SysUserToken userToken = null;
            Entities.SysUser sysUser = null;

            _memoryCache.TryGetValue<Entities.SysUserToken>(token, out userToken);
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


        /// <summary>
        /// 搜索数据
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public IPagedList<Entities.SysUser> searchUser(SysUserSearchArg arg, int page, int size)
        {
            var query = _sysUserRepository.Table.Where(o => !o.IsDeleted);
            if (arg != null)
            {
                if (!String.IsNullOrEmpty(arg.q))
                    query = query.Where(o => o.Account.Contains(arg.q) || o.MobilePhone.Contains(arg.q) || o.Email.Contains(arg.q) || o.Name.Contains(arg.q));
                if (arg.enabled.HasValue)
                    query = query.Where(o => o.Enabled == arg.enabled);
                if (arg.unlock.HasValue)
                    query = query.Where(o => o.LoginLock == arg.unlock);
                if (arg.roleId.HasValue)
                    query = query.Where(o => o.SysUserRoles.Any(r => r.RoleId == arg.roleId));
            }
            query = query.OrderBy(o => o.Account).ThenBy(o => o.Name).ThenByDescending(o => o.CreationTime);
            return new PagedList<Entities.SysUser>(query, page, size);
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entities.SysUser getById(Guid id)
        {
            return _sysUserRepository.getById(id);
        }

        /// <summary>
        /// 新增，插入
        /// </summary>
        /// <param name="model"></param>
        public void insertSysUser(Entities.SysUser model)
        {
            if (existAccount(model.Account))
                return;
            _sysUserRepository.insert(model);
        }

        /// <summary>
        /// 更新修改
        /// </summary>
        /// <param name="model"></param>
        void updateSysUser(Entities.SysUser model)
        {
            _sysUserRepository.DbContext.Entry(model).State = EntityState.Unchanged;
            _sysUserRepository.DbContext.Entry(model).Property("Name").IsModified = true;
            _sysUserRepository.DbContext.Entry(model).Property("Email").IsModified = true;
            _sysUserRepository.DbContext.Entry(model).Property("MobilePhone").IsModified = true;
            _sysUserRepository.DbContext.Entry(model).Property("Sex").IsModified = true;
            _sysUserRepository.DbContext.SaveChanges();
        }

        /// <summary>
        /// 重置密码。默认重置成账号一样
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifer"></param>
        public void resetPassword(Guid id, Guid modifer)
        {

        }

        /// <summary>
        /// 验证账号是否已经存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool existAccount(string account)
        {
            return _sysUserRepository.Table.Any(o => o.Account == account && !o.IsDeleted);
        }

        void ISysUserService.updateSysUser(Entities.SysUser model)
        {
            throw new NotImplementedException();
        }

        public void enabled(Guid id, bool enabled, Guid modifer)
        {
            throw new NotImplementedException();
        }

        public void loginLock(Guid id, bool ulock, Guid modifer)
        {
            throw new NotImplementedException();
        }

        public void deleteUser(Guid id, Guid modifer)
        {
            throw new NotImplementedException();
        }

        public void addAvatar(Guid id, byte[] avatar)
        {
            throw new NotImplementedException();
        }

        public void changePassword(Guid id, string password)
        {
            throw new NotImplementedException();
        }

        public void lastActivityTime(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 移除缓存用户
        /// </summary>
        /// <param name="userId"></param>
        private void removeCacheUser(Guid userId)
        {
            _memoryCache.Remove(String.Format(MODEL_KEY, userId));
        }



    }
}
