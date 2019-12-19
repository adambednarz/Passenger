﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Passenger.Infrastructure.Extensions;

namespace Passenger.Infrastructure.Services
{
    public class Encrypter : IEncrypter
    {
        private static readonly int DerivedBytesIterationsCount = 10000;
        private static readonly int SaltSize = 40;

        public string GetSalt(string value)
        {
            if (value.Empty())
            {
                throw new ArgumentException("Could not generate hash from empty value");
            }

            var random = new Random();
            var saltBytes = new byte[SaltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public string GetHash(string value, string salt)
        {
            if(value.Empty())
            {
                throw new ArgumentException("Could not generate hash from empty value");
            }
            if (salt.Empty())
            {
                throw new ArgumentException("Could not generate hash from empty salt value");
            }

            var pbkdf2 = new Rfc2898DeriveBytes(value, GetBytes(salt), DerivedBytesIterationsCount);

            return Convert.ToBase64String(pbkdf2.GetBytes(SaltSize));

        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}
