using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileArts.api.Domain.Services
{
    public interface IUserPasswordService
    {
        string ComputeHash(string plainText, string hashAlgorithm, byte[] saltBytes);
         string CreateRandomPassword(int PasswordLength);
         int CreateRandomSalt();
        byte[] CreateRandomSaltbyte(); 
        bool VerifyHash(string plainText, string hashAlgorithm, string hashValue);
    }
}
