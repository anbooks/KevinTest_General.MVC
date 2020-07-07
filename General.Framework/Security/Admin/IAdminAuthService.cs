using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework.Security.Admin
{
   /// <summary>
   /// 登录安全相关的都放在这边
   /// </summary>
    public  interface IAdminAuthService
    {   /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <param name="name"></param>
        void signIn(string token, string name);



        /// <summary>
        /// 退出登录
        /// </summary>
        void signOut();


        /// <summary>
        /// 获取当前登录用户
        /// 缓存起来的
        /// </summary>
        /// <returns></returns>
        Entities.SysUser getCurrentUser();

        /// <summary>
        /// 获取我的权限数据
        /// </summary>
        /// <returns></returns>
        List<Entities.Category> getMyCategories();

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        bool authorize(ActionExecutingContext context);

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="routeName"></param> 
        /// <returns></returns>
        bool authorize(string routeName);

    }
}
