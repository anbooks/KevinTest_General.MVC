using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace General.Entities.Category
{
    [Table("Category")]   //这个才是数据的表
    public class Category
    {

        public int Id { get; set; }
        [Required(ErrorMessage="要输入啊亲")]
        public string Name { get; set; }
        public bool  IsMenu { get; set; }   //是不是导航栏，是的话就在侧面显示，用控制器和Action  
        public string SysResource { get; set; }   //页面的唯一标识
        public string ResourceID { get; set; }  //
        public string FatherResource { get; set; }
        public string FatherID { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string RouteName { get; set; }
        public string CssClass { get; set; }  //菜单的图标
        public int Sort { get; set; }
        public bool IsDisabled { get; set; }



    }
}
