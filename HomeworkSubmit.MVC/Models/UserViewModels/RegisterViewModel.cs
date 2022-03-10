using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeworkSubmit.MVC.Models.UserViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="用户名")]
        [StringLength(40, MinimumLength = 2)]
        public string LoginName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name ="密码")]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        [Display(Name ="确认密码")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name ="姓名")]
        public string Name { get; set; }
        [Required]
        [Display(Name ="班级")]
        public int ClassNum { get; set; }
    }
}