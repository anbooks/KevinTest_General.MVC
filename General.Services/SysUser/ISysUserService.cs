using System;
using System.Collections.Generic;
using System.Text;

namespace General.Services.SysUser
{
    public  interface ISysUserService
    {
        List<Entities.SysUser.SysUser> getAll();

        /// <summary>
        /// 验证登录状态
        /// </summary>
        /// <param name="account">登录账号</param>
        /// <param name="password">密码</param>
        /// <param name="r">随机数</param>
        /// <returns></returns>
        (bool Status,string Message,string Token,Entities.SysUser.SysUser User) validateUser(string account,string password,string r);

        /// <summary>
        /// 通过账号获取用户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Entities.SysUser.SysUser getByAccount(string account);
    }

    public interface IGeneralService
    {

    }
}
