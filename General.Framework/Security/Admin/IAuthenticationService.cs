using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework.Security.Admin
{
   /// <summary>
   /// 登录安全相关的都放在这边
   /// </summary>
    public  interface IAuthenticationService
    {   /// <summary>
    /// 获取当前登录用户
    /// 缓存起来的
    /// </summary>
    /// <returns></returns>
        Entities.SysUser.SysUser getCurrentUser();
    }
}
