﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace BLL
{
    public class Hashing
    {
        public static Guid GetHashString(string text)
        {
            var bytes = Encoding.Unicode.GetBytes(text);

            var csp = new MD5CryptoServiceProvider();

            var byteHash = csp.ComputeHash(bytes);

            var hash = string.Empty;

            foreach (var b in byteHash)
                hash += $"{b:x2}";

            return new Guid(hash);
        }
    }
}