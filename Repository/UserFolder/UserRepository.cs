using ManagementAssistanceForBusinessWeb_OnlyRole.Context;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models;
using ManagementAssistanceForBusinessWeb_OnlyRole.Models.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using static ManagementAssistanceForBusinessWeb_OnlyRole.Models.UserModel;

namespace ManagementAssistanceForBusinessWeb_OnlyRole.Repository.UserFolder
{
    public class UserRepository : IUserRepository
    {
        private readonly ModelDbContext _context;
        private readonly IPasswordHasher<UserModel> _passwordHasher;
        //private readonly IPasswordHasher<RegisterAccountForUserViewModel> _passwordHasherUser;

        public UserRepository(ModelDbContext context, IPasswordHasher<UserModel> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            //_passwordHasherUser = passwordHasherUser;
        }
        public void CreateAccountForAdmin(CreateAccountForAdminViewModel newAccount)
        {
            var account = new UserModel()
            {
                Username = newAccount.Username,
                Email = newAccount.Email,
                PhoneNumber = newAccount.PhoneNumber,
                Role = newAccount.Role,
                Password = _passwordHasher.HashPassword(new UserModel(), newAccount.Password)
            };
            _context.Add(account);
            _context.SaveChanges();
        }

        public void RegisterAccountForUser(RegisterAccountForUserViewModel newAccount)
        {
            var account = new UserModel()
            {
                Username = newAccount.Username,
                Email = newAccount.Email,
                PhoneNumber = newAccount.PhoneNumber,
                Role = newAccount.Role,
                Password = _passwordHasher.HashPassword(new UserModel(), newAccount.Password)
            };
            _context.Add(account);
            _context.SaveChanges();
        }
        public UserModel FindUser(string email, string password)
        {
            
            UserModel user = _context.Members.FirstOrDefault(u => email.Equals(u.Email));

            if (user != null)
            {
                // Verify the password
                PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

                if (result == PasswordVerificationResult.Success)
                {
                    return user;
                }
            }
            return null;
        }

        public async  Task<IEnumerable<UserModel>> GetUsersByRoleAsync()
        {
            return await _context.Members.Where(u => u.Role == ERole.User).ToListAsync();
        }

        // hiển thị danh sách tất cả user bao gồm cả user và admin => chi co admin mới xem được 
		public async Task<IEnumerable<UserModel>> GetAllUsers()
		{
            return await _context.Members.ToListAsync();
		}
        public async Task<UserModel> GetUserByID(int userID)
        {
            return await _context.Members.FirstOrDefaultAsync(u => u.UserID == userID);
        }

      
    }
}
