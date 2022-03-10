using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeworkSubmit.MVC.Models.HomeworkViewModels
{
    public class AddCommentViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
    }
}