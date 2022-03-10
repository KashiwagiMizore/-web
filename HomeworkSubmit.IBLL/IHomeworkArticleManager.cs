using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.IBLL
{
    public interface IHomeworkArticleManager
    {
        Task AddArticleAsync(string title, string content, Guid versionId, Guid userId, Guid classNumId);
        Task AddVersionAsync(string versionName, Guid userId, int limitDays, Guid classNumId);
        Task<List<DTO.HomeworkVersionDTO>> GetAllVersionsAsync();
        Task<List<DTO.HomeworkArticleDTO>> GetAllHomeworkArticlesByUserIdAsync(Guid userId);
        Task<List<DTO.HomeworkArticleDTO>> GetAllHomeworkArticlesByLoginNameAsync(string loginName);
        Task<List<DTO.HomeworkArticleDTO>> GetAllHomeworkArticlesByVersionIdAsync(Guid versionId);
        Task<List<DTO.HomeworkArticleDTO>> GetAllHomeworkArticlesAsync();
        Task TrueRemoveOneVersionAsync(Guid versionId);
        Task EditVersionAsync(Guid versionId, string newVersionName, int limitDays);
        Task TrueRemoveOneArticleAsync(Guid articleId);
        Task EditArticleAsync(Guid articleId, string title, string content);
        Task<bool> ExistVersionAsync(Guid versionId);
        Task<bool> ExistArticleAsync(Guid articleId);
        Task<DTO.HomeworkArticleDTO> GetOneArticleByArticleIdAsync(Guid articleId);
        Task<DTO.HomeworkVersionDTO> GetOneVersionByVersionIdAsync(Guid versionId);
        Task AddCommentAsync(Guid userId, Guid articleId, string content);
        Task<List<DTO.TeacherCommentDTO>> GetAllCommentsByArticleIdAsync(Guid articleId);
        Task TrueRemoveAllCommentsByArticleIdAsync(Guid articleId);
        Task TrueRemoveAllArticlesByVersionIdAsync(Guid versionId);
        Task<bool> ExistAnyArticleInThisVersionAsync(Guid versionId);
        Task EditScoreAsync(Guid userId, Guid articleId, int score);
        Task<bool> JudgeAuthorisTeacherAsync(Guid articleId);
        Task<bool> JudgeAuthorisMyselfAsync(Guid articleId,Guid userId);

    }
}
