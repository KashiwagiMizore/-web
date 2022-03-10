using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.DTO
{
    public class HomeworkScoreDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string LoginName { get; set; }
        public Guid ArticleId { get; set; }
        public int Score { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
