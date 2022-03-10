using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.Models
{
    public class Notice:BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required, Column(TypeName = "ntext")]//调整类型为ntext 防止放不下文章内容
        public string Content { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
