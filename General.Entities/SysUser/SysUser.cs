using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace General.Entities.SysUser
{
    [Table("SysUser")]   //这个才是数据的表
    public class SysUser
    {
        public string  Id { get; set; }

        public string Account { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
