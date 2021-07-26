using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Utility
{
    public static class Protector
    {
        private static readonly byte[] salt = Encoding.Unicode.GetBytes("7BANANAS"); // Уникальные соли можно будет позже добавить в бд

        public static string GetSafePassword(string password)
        { 
            var saltText = Convert.ToBase64String(salt);
            var sha = SHA256.Create();
            var saltedPassword = password + saltText;
            var saltedhashedPassword = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
            return saltedhashedPassword;
        }
    }
}
