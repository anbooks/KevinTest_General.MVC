using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Entities
{
   public  class GeneralDbContext:DbContext
    {
        public GeneralDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category.Category> Categories { get; set; }
        // public DbSet<Category> Categories { get; set; }



       
        public DbSet<SysUser.SysUser> SysUsers { get; set; }
        public DbSet<SysUserToken.SysUserToken> SysUserTokenes { get; set; }
        public DbSet<SysUserLoginLog.SysUserLoginLog> SysUserLoginLogs { get; set; }
        public DbSet<SysUserRole.SysUserRole> SysUserRoles { get; set; }
        public DbSet<SysPermission.SysPermission> SysPermissions { get; set; }
    }
}
