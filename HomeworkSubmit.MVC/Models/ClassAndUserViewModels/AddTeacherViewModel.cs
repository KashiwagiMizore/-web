using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeworkSubmit.MVC.Models.ClassAndUserViewModels
{
    public class AddTeacherViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "用户名")]
        [StringLength(40, MinimumLength = 2)]
        public string LoginName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }
    }
}