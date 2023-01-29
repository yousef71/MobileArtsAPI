using MobileArts.api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileArts.api.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ListAll();
        Task<User> Add(User user);
        Task<User> Update(User user);
        Task<User> Delete(int id);
        Task<User> GetById(int id);
        Task<User> GetUserWithHistory(string UserName);
        Task<User> GetUserLoginHistory(string UserName);
        Task<User> UserLogin(LogInRequest user);
        Task<User> ResetPwd(ChangePasswordRequest user);
        Task<User> ChangePwd(ChangePasswordRequest user);

    }
}
