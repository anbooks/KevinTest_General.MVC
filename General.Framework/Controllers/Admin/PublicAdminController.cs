using General.Framework.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework.Controllers.Admin
{
    [AdminAuthFilter]
    /// <summary>
    /// 后台，前后台分离
    /// </summary>
    /// public class PublicAdminController:BaseController
    
    public class PublicAdminController: AdminAreaController
    {

    }
}
