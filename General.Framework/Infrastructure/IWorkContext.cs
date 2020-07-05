using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework.Infrastructure
{
    //获取当前登录状态
    public interface  IWorkContext
    {
        Entities.SysUser.SysUser CurrentUser { get; }
    }
}
