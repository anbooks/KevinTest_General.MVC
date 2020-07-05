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
        (bool,string,string,Entities.SysUser.SysUser) validateUser(string account,string password,string r);
    }

    public interface IGeneralService
    {

    }
}
