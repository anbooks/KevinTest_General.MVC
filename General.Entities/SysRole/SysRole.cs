﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace General.Entities.SysRole
{
    [Table("SysRole")]
    public partial class SysRole
    {
        public SysRole()
        {
            SysPermission = new HashSet<Entities.SysPermission.SysPermission>();
            SysUserRole = new HashSet<Entities.SysUserRole.SysUserRole>();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "请输入角色名称")]
        [StringLength(500)]
        public string Name { get; set; }

        public Guid Creator { get; set; }

        public DateTime CreationTime { get; set; }

        public Guid? Modifier { get; set; }

        public DateTime? ModifiedTime { get; set; }


        public virtual ICollection<Entities.SysPermission.SysPermission> SysPermission { get; set; }

        [ForeignKey("Creator")]
        public virtual Entities.SysUser.SysUser SysUser { get; set; }

        [ForeignKey("Modifier")]
        public virtual Entities.SysUser.SysUser SysUser1 { get; set; }

        public virtual ICollection<Entities.SysUserRole.SysUserRole> SysUserRole { get; set; }
    }

}
