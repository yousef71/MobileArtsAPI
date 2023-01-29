using MobileArts.api.Domain.Models;
using MobileArts.api.Domain.Repositories;
using MobileArts.api.Domain.Services;
using MobileArts.api.Persistence.Contexts;
using Dentrroper.Mobile.api.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileArts.api.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly IUserPasswordService _userPasswordService;

        public UserRepository(AppDataContext context, IUserPasswordService userPasswordService) : base(context)
        {
            _userPasswordService = userPasswordService;
        }

        public async Task<User> Add(User user)
        {  
            byte[] salt = _userPasswordService.CreateRandomSaltbyte();
            user.UserPassword = _userPasswordService.ComputeHash(user.UserPassword, "SHA512", salt);

            var result = await  _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User> Delete(int id)
        {
            if (_context.Users.Any(u => u.UserId == id))
            {
                User user = await _context.Users.FindAsync(id);
                user.Active = false;
                //var result = _context.Users.Remove(user);
                var result = _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            else
                return null;

        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.Where(u => u.UserId == id).Where(u => u.Active == true)
            .Include(u => u.Member)
            .Include(u => u.UserRole)
            .ThenInclude(ur => ur.RolePermissions)
            .ThenInclude(up => up.Permission)
            .FirstOrDefaultAsync();

        }
        public async Task<User> GetUserWithHistory(string UserName)
        {
            return await _context.Users.Where(u => u.UserName == UserName).Where(u => u.Active == true)
            .Include(u => u.Member)
            .Include(u => u.UserRole)
            .ThenInclude(ur => ur.RolePermissions)
            .ThenInclude(up => up.Permission)
            .Include(u => u.UserLogins)
            .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserLoginHistory(string UserName)
        {
            return await _context.Users.Where(u => u.UserName == UserName).Where(u => u.Active == true)
                  .Include(u => u.UserLogins)
                  .FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<User>> ListAll()
        {
            return await _context.Users.Where(u => u.Active == true).ToListAsync();
        }

        public async Task<User> Update(User user)
        {
            if (_context.Users.Any(u => u.UserId == user.UserId))
            {
                var result = _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            else
                return null;

        }
        public async Task<User> ResetPwd(ChangePasswordRequest user)
        {
            User UpdateUser = await _context.Users.Where(u => u.UserName == user.UserName).FirstOrDefaultAsync();
            if ((UpdateUser != null) && (user.NewPassword == user.ConfirmNewPassword))
            {
                byte[] salt = _userPasswordService.CreateRandomSaltbyte();
                UpdateUser.UserPassword = _userPasswordService.ComputeHash(user.NewPassword, "SHA512", salt);

                var result = _context.Users.Update(UpdateUser);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            else
                return null;
        }

        public async Task<User> ChangePwd(ChangePasswordRequest user)
        {
            User DbUser = await _context.Users.Where(u => u.UserName == user.UserName).FirstOrDefaultAsync();
            if (DbUser != null)
            {
                string paswStr = DbUser.UserPassword;
                if (_userPasswordService.VerifyHash(user.UserPassword, "SHA512", paswStr) && (user.NewPassword == user.ConfirmNewPassword))
                {
                    byte[] salt = _userPasswordService.CreateRandomSaltbyte();
                    DbUser.UserPassword = _userPasswordService.ComputeHash(user.NewPassword, "SHA512", salt);
                    var result = _context.Users.Update(DbUser);
                    await _context.SaveChangesAsync();
                    return result.Entity;
                }

                return null;
            }

            else
                return null;
        }

        public async Task<User> UserLogin(LogInRequest user)
        {
            User logInUser=await _context.Users.Where(u => u.UserName == user.UserName).FirstOrDefaultAsync();
            if (logInUser != null)
            {
                string paswStr = logInUser.UserPassword;
                if (_userPasswordService.VerifyHash(user.UserPassword, "SHA512", paswStr))
                {

                    UserLogin userLogin = new Domain.Models.UserLogin();
                    userLogin.UserId = logInUser.UserId;
                    userLogin.UserName = logInUser.UserName;
                    userLogin.Pasw = paswStr;
                    userLogin.LoginDate = DateTime.Now;
                    userLogin.LoginExpiryDate = logInUser.LoginExpiryDate;

                    _context.UserLogins.Add(userLogin);
                    await _context.SaveChangesAsync();

                    return logInUser;
                }

                return null;
            }
            
            else
                return null;

        }

    }
}
