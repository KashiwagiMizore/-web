using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSubmit.IBLL
{
    public interface IUserManager
    {
        Task<bool> RegisterAsync(string loginName, string password, int classNum, string name, Guid classNumId);
        bool Login(string loginName, string password, out Guid userId, out bool isTeacher,
            out bool isAdmin, out bool isStudent, out int classNum, out Guid classNumId);
        Task ChangeInformationAsync(string loginName, string oldPwd, string newPwd,string name);//用户自行修改用
        Task AdminChangeUserInformationAsync(bool isAdmin, Guid userId, string name, string password
            , Guid classNumId, bool isStudent, bool isTeacher, int classNum);//管理员修改用
        Task<DTO.UserInformationDTO> GetUserByLoginNameAsync(string loginName);
        Task<bool> ExistClassNumAsync(int classNum);
        Task<List<DTO.ClassNumberDTO>> GetAllClassOrderAsync();
        Task<List<DTO.UserInformationDTO>> GetAllUserByClassNumIdAsync(Guid classNumId);
        Task<List<DTO.UserInformationDTO>> GetAllUserAsync();
        Task<Guid> GetClassIdByClassNumAsync(int classNum);
        Task<DTO.ClassNumberDTO> GetClassByClassNumIdAsync(Guid classNumId);
        Task ChangeClassNumAsync(Guid classNumId, int classNum);
        Task AddTeacherAsync(string loginName, string password, string name, Guid classNumId);
        Task<bool> ExistUserAsync(string loginName);
        Task TrueRemoveUserByUserIdAsync(Guid userId);
        Task<bool> ExistAnyArticlesByUserIdAsync(Guid userId);
        Task<bool> ExistAnyVersionsByUserIdAsync(Guid userId);
        Task AddClassAsync(int classNum);
        Task<bool> ExistAnyUsersInThisClassAsync(Guid classNumId);
        Task TrueRemoveClassByClassNumIdAsync(Guid classNumId);
    }
}
