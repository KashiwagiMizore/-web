using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeworkSubmit.MVC.Models.ClassAndUserViewModels
{
    public class EditClassViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "班级")]
        public int ClassNum { get; set; }
    }
}