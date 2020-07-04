using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace General.Core.Librs
{   /// <summary>
    ///通过程序集的名称  加载程序集 反射
    /// </summary>
    //把服务映像 映射  
     public class RuntimeHelper
    {
        public static Assembly  GetAssemblyByName(string assemblyName)
        {
          return   AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
        }
    }
}
