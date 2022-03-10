using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.Models
{
    public class HomeworkVersion:BaseEntity
    {
        [Required]
        [StringLength(200,MinimumLength =2)]
        public string VersionName { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int LimitDays { get; set; }//添加该次作业的过期时间
        [ForeignKey(nameof(ClassNumber))]
        public Guid ClassNumberId { get; set; }
        public ClassNumber ClassNumber { get; set; }
        //public Guid? ArticleIds { get; set; }//数据库不支持数组 采用这种方式代替 此处并非外键 只是获取所有的文章id来供删除使用
    }
}
