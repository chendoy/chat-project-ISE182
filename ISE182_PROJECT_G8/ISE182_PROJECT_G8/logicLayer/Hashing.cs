﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ISE182_PROJECT_G8.logicLayer
{

    /*
     Used to produce Hashed password for authentication
     and validating the password according to the requirments
     */

    class Hashing
    {
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }

        public static bool isValid(string password)
        {
            if (password.Length < 4 || password.Length > 16)
                return false;
           return password.All(c => Char.IsLetterOrDigit(c));
           
        }


    }
}
