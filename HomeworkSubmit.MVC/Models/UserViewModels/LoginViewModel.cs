using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeworkSubmit.MVC.Models.UserViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(40, MinimumLength = 2)]
        [Display(Name ="用户名")]
        public string LoginName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 2)]
        [Display(Name ="密码")]
        public string Password { get; set; }

        [Display(Name ="记住密码")]
        public bool RememberMe { get; set; }
    }
}