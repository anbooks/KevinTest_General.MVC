using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework.Filters
{
    /// <summary>
    /// 登录状态过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple = true, Inherited = true)]
    public class AdminAuthFilter : Attribute,IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
           // throw new NotImplementedException();
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
           // throw new NotImplementedException();
        }
    }
}
