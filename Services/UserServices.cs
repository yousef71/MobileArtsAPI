using MobileArts.api.Domain.Models;
using MobileArts.api.Domain.Repositories;
using MobileArts.api.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileArts.api.Services
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Add(User user)
        {
          return await _userRepository.Add(user);
        }

        public  async Task<User> Delete(int id)
        {
            return await _userRepository.Delete(id);
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<User> GetUserWithHistory(string UserName)
        {
            return await _userRepository.GetUserWithHistory(UserName);
        }
        public async Task<User> GetUserLoginHistory(string UserName)
        {
            return await _userRepository.GetUserLoginHistory(UserName);
        }

        public async Task<IEnumerable<User>> ListAll()
        {
            return await _userRepository.ListAll() ;
        }

        public async Task<User> Update(User user)
        {
            return await _userRepository.Update(user);
        }

        public async Task<User> UserLogin(LogInRequest user)
        {
            return await _userRepository.UserLogin(user);
        }
        public async Task<User> ResetPwd(ChangePasswordRequest user)
        {
            return await _userRepository.ResetPwd(user);
        }

        public async Task<User> ChangePwd(ChangePasswordRequest user)
        {
            return await _userRepository.ChangePwd(user);
        }
    }
}
