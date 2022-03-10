using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.Models
{
    public class HomeworkArticle:BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required,Column(TypeName ="ntext")]//调整类型为ntext 防止放不下文章内容
        public string Content { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(HomeworkVersion))]
        public Guid VersionId { get; set; }
        public HomeworkVersion HomeworkVersion { get; set; }

        public int Score { get; set; }
        [ForeignKey(nameof(ClassNumber))]
        public Guid ClassNumberId { get; set; }
        public ClassNumber ClassNumber { get; set; }
    }
}
