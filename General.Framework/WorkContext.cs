using General.Entities.SysUser;
using General.Framework.Infrastructure;
using General.Framework.Security.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework
{
    public  class WorkContext:IWorkContext
    {
        private IAuthenticationService _authenticationService;



        public WorkContext(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public SysUser CurrentUser
        {
            get { return _authenticationService.getCurrentUser(); }

        }
    }
}
