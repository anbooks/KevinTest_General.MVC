using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework.Infrastructure
{
    //获取当前登录状态
    public interface  IWorkContext
    {
        Entities.SysUser CurrentUser { get; }

        /// <summary>
        /// 当前登录用户的菜单
        /// </summary>
        List<Entities.Category> Categories { get; }
    }
}
