using System;
using System.Collections.Generic;
using System.Text;

namespace General.Entities
{
    public class RolePermissionViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Entities.SysRole Role { get; set; }

        /// <summary>
        /// 角色select下拉菜单
        /// </summary>
        public List<Entities.SysRole> RoleList { get; set; }

        /// <summary>
        /// 角色的权限数据
        /// </summary>
        public List<Entities.SysPermission> Permissions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Entities.Category> CategoryList { get; set; }
    }
}
