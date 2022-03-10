using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeworkSubmit.MVC.Models.UserViewModels
{
    public class EditUserInformationViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "旧密码")]
        public string OldPwd { get; set; }
        [Required]
        [Display(Name = "新密码")]
        public string NewPwd { get; set; }
        [Required]
        [Compare(nameof(NewPwd))]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }
    }
}