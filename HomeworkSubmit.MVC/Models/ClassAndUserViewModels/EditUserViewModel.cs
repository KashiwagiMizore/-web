using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeworkSubmit.MVC.Models.ClassAndUserViewModels
{
    public class EditUserViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }//昵称
        [Required]
        [Display(Name = "班级")]
        public int ClassNum { get; set; }
        public bool IsStudent { get; set; }
        public bool IsTeacher { get; set; }
    }
}