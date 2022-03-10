using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeworkSubmit.MVC.Models.NoticeViewModels
{
    public class AddNoticeViewModel
    {
        public Guid Id { get; set; }//此处的Id即为进入类别的时候传过来的Id值 和get中的Guid? id中的id相等 可以通过调试测试得到
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}