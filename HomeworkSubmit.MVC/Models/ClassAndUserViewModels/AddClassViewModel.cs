using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeworkSubmit.MVC.Models.ClassAndUserViewModels
{
    public class AddClassViewModel
    {
        public Guid Id { get; set; }//5.2发现遗漏补加
        [Required]
        [Display(Name = "班级")]
        public int ClassNum { get; set; }
    }
}