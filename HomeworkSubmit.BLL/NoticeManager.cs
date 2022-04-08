using HomeworkSubmit.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.BLL
{
    public class NoticeManager : IBLL.INoticeManager
    {
        public async Task AddNoticeAsync(string title, string content, Guid userId)
        {
            using (var noticeService=new DAL.NoticeService())
            {
                var noticeData = new Models.Notice()
                {
                    Title = title,
                    Content = content,
                    UserId = userId
                };
                await noticeService.AddAsync(noticeData);
            }
        }

        public async Task ChangeNoticeAsync(Guid noticeId, string title, string content)
        {
            using (var noticeService=new DAL.NoticeService())
            {
                var noticeData =await noticeService.GetOneByIdAsync(noticeId);
                noticeData.Title = title;
                noticeData.Content = content;
                await noticeService.EditAsync(noticeData);
            }
        }

        public async Task<List<NoticeDTO>> GetAllNoticesOrderAsync()
        {
            using (var noticeService = new DAL.NoticeService())
            {
                return await noticeService.GetAllByOrder().Include(m => m.User).Select(m => new DTO.NoticeDTO()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Content = m.Content,
                    CreateTime = m.CreateTime,
                    Name = m.User.Name
                }).ToListAsync();
            }
        }

        public async Task<NoticeDTO> GetOneNoticeByNoticeIdAsync(Guid noticeId)
        {
            using (var noticeService = new DAL.NoticeService())
            {
                return await noticeService.GetAll().Where(m => m.Id == noticeId).Include(m => m.User)
                    .Select(m => new DTO.NoticeDTO()
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Content = m.Content,
                        CreateTime = m.CreateTime,
                        Name = m.User.Name
                    }).FirstAsync();
            }
        }

        public async Task TrueRemoveOneNoticeAsync(Guid noticeId)
        {
            using (var noticeService=new DAL.NoticeService())
            {
                var noticeData =await noticeService.GetOneByIdAsync(noticeId);
                await noticeService.TrueRemoveAsync(noticeData);
            }
        }
    }
}
