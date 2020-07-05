using System;
using System.Collections.Generic;
using System.Text;

namespace General.Services.SysUser
{
  public  interface ISysUserService
    {
        List<Entities.SysUser.SysUser> getAll();
    }

    public interface IGeneralService
    {

    }
}
