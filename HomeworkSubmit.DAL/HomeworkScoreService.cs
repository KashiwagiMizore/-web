using HomeworkSubmit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.DAL
{
    public class HomeworkScoreService : BaseService<Models.HomeworkScore>
    {
        private readonly Models.HomeworkContext _dbScore;
        public HomeworkScoreService(Models.HomeworkContext db) : base(new HomeworkContext())
        {
            _dbScore = db;
        }
        public IQueryable<Models.HomeworkScore> GetAllByArticleId(Guid articleId)
        {
            return _dbScore.Set<HomeworkScore>()
                .Where(m => m.IsRemoved == false && m.HomeworkArticleId == articleId).AsNoTracking();
        }
    }
}
