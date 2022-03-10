using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeworkSubmit.MVC.Models.HomeworkViewModels
{
    public class AddVersionViewModel
    {
        [Required]
        [StringLength(200, MinimumLength = 2)]
        [Display(Name ="第几次作业")]
        public string VersionName { get; set; }
        [Required]
        [Display(Name = "限制天数")]
        public int LimitDays { get; set; }
    }
}