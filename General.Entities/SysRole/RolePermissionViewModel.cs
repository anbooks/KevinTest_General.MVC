using System;
using System.Collections.Generic;
using System.Text;

namespace General.Entities.RolePermissionViewModel
{
    public class RolePermissionViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Entities.SysRole.SysRole Role { get; set; }

        /// <summary>
        /// 角色select下拉菜单
        /// </summary>
        public List<Entities.SysRole.SysRole> RoleList { get; set; }

        /// <summary>
        /// 角色的权限数据
        /// </summary>
        public List<Entities.SysPermission.SysPermission> Permissions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Entities.Category.Category> CategoryList { get; set; }
    }
}
