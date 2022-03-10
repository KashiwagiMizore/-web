using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeworkSubmit.MVC.Models.HomeworkViewModels
{
    public class EditVersionViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        [Display(Name ="第几次作业")]
        public string VersionName { get; set; }
        [Required]
        public int LimitDays { get; set; }
    }
}