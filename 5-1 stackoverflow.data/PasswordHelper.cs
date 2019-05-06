using System;
using System.Collections.Generic;
using System.Text;

namespace _5_1_stackoverflow.data
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool PasswordMatch(string userInput, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(userInput, passwordHash);
        }


    }
}
