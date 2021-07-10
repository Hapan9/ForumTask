using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BLL.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace BLL
{
    public class JwtGenerator
    {
        public static string GenerateJwt(UserDto userDto)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                AuthOptions.Issuer,
                AuthOptions.Audience,
                notBefore: now,
                claims: new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userDto.Name),
                    new Claim("surname", userDto.Surname),
                    new Claim("login", userDto.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, userDto.Role.ToString())
                },
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}