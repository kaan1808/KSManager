using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KSManager.Helper
{
    public static class PasswordHelper
    {
        private const int SaltByteSize = 32;
        private const int HashByteSize = 20;
        private const int Pbkdf2Iteration = 100000;
        private const int IterationIndex = 0;
        private const int SaltIndex = 1;
        private const int Pbkdf2Index = 2;

        public static Tuple<byte[], byte[]> HashPassword(string password)
        {
            var salt = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iteration, HashByteSize);
            return Tuple.Create(hash, salt);

        }


        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iteration, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt)
            {
                IterationCount = iteration
            };
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
