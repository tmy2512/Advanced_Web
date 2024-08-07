using static ManagementAssistanceForBusinessWeb_OnlyRole.Models.TaskModel;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.UserViewModels;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Repository.UserFolder
{
    public interface IUserRepository
    {
        void RegisterAccountForUser(RegisterAccountForUserViewModel newAccount);
        void CreateAccountForAdmin(CreateAccountForAdminViewModel newAccount);
        UserModel FindUser(String email, String password);
        Task<IEnumerable<UserModel>> GetUsersByRoleAsync();
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<UserModel> GetUserByID(int userID);

    }
}
