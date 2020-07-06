using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace General.Entities.SysUserRole
{
    [Table("SysUserRole")]
    public partial class SysUserRole
    {
        public Guid Id { get; set; }


        public Guid RoleId { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Entities.SysRole.SysRole SysRole { get; set; }

        [ForeignKey("UserId")]
        public virtual Entities.SysUser.SysUser SysUser { get; set; }
    }

}
