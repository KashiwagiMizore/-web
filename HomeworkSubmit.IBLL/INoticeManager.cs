using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.IBLL
{
    public interface INoticeManager
    {
        Task AddNoticeAsync(string title, string content, Guid userId);
        Task<List<DTO.NoticeDTO>> GetAllNoticesOrderAsync();
        Task<DTO.NoticeDTO> GetOneNoticeByNoticeIdAsync(Guid noticeId);
        Task ChangeNoticeAsync(Guid noticeId, string title, string content);
        Task TrueRemoveOneNoticeAsync(Guid noticeId);
    }
}
