using General.Framework.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework.Controllers.Admin
{
   /// <summary>
   ///具有权限，需要权限验证的过滤器继承
   /// </summary>
    [PermissionActionFilter]
   public class AdminPermissionController:PublicAdminController
    {
    }
}
