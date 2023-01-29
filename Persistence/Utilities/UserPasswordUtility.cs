using MobileArts.api.Domain.Repositories;
using MobileArts.api.Domain.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MobileArts.api.Services
{
    public class UserPasswordUtility : IUserPasswordService
    {
        private readonly string _RandomPassword;

        public UserPasswordUtility(IConfiguration configuration)
        {
            _RandomPassword = configuration.GetSection("RandomPassword").Get<string>();
        }

        public string CreateRandomPassword(int PasswordLength)
        {
            String _allowedChars = _RandomPassword;
            Byte[] randomBytes = new Byte[PasswordLength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }

        public int CreateRandomSalt()
        {
            byte[] _saltBytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(_saltBytes);

            return ((((int)_saltBytes[0]) << 24) + (((int)_saltBytes[1]) << 16) +
              (((int)_saltBytes[2]) << 8) + ((int)_saltBytes[3]));
        }
        public byte[] CreateRandomSaltbyte()
        {
            byte[] _saltBytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(_saltBytes);

            var intresult = ((((int)_saltBytes[0]) << 24) + (((int)_saltBytes[1]) << 16) +
              (((int)_saltBytes[2]) << 8) + ((int)_saltBytes[3]));

            return _saltBytes;
        }

        public string ComputeHash(string plainText, string hashAlgorithm, byte[] saltBytes)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] plainTextSaltBytes =
                    new byte[plainTextBytes.Length + saltBytes.Length];

            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextSaltBytes[i] = plainTextBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                plainTextSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            HashAlgorithm hash;

            switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hash = new SHA1Managed();
                    break;

                case "SHA256":
                    hash = new SHA256Managed();
                    break;

                case "SHA384":
                    hash = new SHA384Managed();
                    break;

                case "SHA512":
                    hash = new SHA512Managed();
                    break;

                default:
                    hash = new MD5CryptoServiceProvider();
                    break;
            }

            byte[] hashBytes = hash.ComputeHash(plainTextSaltBytes);

            byte[] hashSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            for (int i = 0; i < hashBytes.Length; i++)
                hashSaltBytes[i] = hashBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                hashSaltBytes[hashBytes.Length + i] = saltBytes[i];

            string hashValue = Convert.ToBase64String(hashSaltBytes);

            return hashValue;

        }
        public bool VerifyHash(string plainText, string hashAlgorithm, string hashValue)
        {
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            int hashSizeInBits, hashSizeInBytes;

            switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hashSizeInBits = 160;
                    break;

                case "SHA256":
                    hashSizeInBits = 256;
                    break;

                case "SHA384":
                    hashSizeInBits = 384;
                    break;

                case "SHA512":
                    hashSizeInBits = 512;
                    break;

                default: // MD5
                    hashSizeInBits = 128;
                    break;
            }

            hashSizeInBytes = hashSizeInBits / 8;

            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            byte[] saltBytes = new byte[hashWithSaltBytes.Length -
                                        hashSizeInBytes];

            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            string expectedHashString =
                        ComputeHash(plainText, hashAlgorithm, saltBytes);

            return (hashValue == expectedHashString);

        }
    }
}
