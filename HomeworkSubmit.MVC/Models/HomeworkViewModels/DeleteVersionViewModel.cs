using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeworkSubmit.MVC.Models.HomeworkViewModels
{
    public class DeleteVersionViewModel
    {
        public Guid Id { get; set; }
        //public Guid[] ArticleIds { get; set; } //删除某个类别所有文章的评论用
    }
}