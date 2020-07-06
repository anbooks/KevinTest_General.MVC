﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace General.Entities.SysUserToken
{
    [Serializable]
    [Table("SysUserToken")]
    public partial class SysUserToken
    {
        public Guid Id { get; set; }

        public Guid SysUserId { get; set; }

        public DateTime ExpireTime { get; set; }

        [ForeignKey("SysUserId")]
        public virtual Entities.SysUser.SysUser SysUser { get; set; }
    }

}
