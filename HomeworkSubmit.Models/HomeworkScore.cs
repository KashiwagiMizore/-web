using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.Models
{
    public class HomeworkScore:BaseEntity
    {
        [Required]
        public int Score { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(HomeworkArticle))]
        public Guid HomeworkArticleId { get; set; }
        public HomeworkArticle HomeworkArticle { get; set; }
    }
}
