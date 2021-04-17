using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class AuthOptions
    {
        public const string ISSUER = "Client";
        public const string AUDIENCE = "Client";
        const string KEY = "epam forum project";
        public const int LIFETIME = 5;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
