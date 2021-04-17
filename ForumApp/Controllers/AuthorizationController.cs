using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL;
using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PL.Models;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserService _workWithUser;

        public AuthorizationController(IUserService workWithUser)
        {
            _workWithUser = workWithUser;
        }

        [HttpPost]
        public async Task<IActionResult> UserAuthorize([FromBody] AuthorizationDto authorizationDto)
        {
            try
            {
                var user = await _workWithUser.CheckUserForm(authorizationDto);

                if (user == null) return NotFound("Wrong password");

                var now = DateTime.UtcNow;

                var jwt = new JwtSecurityToken(
                    AuthOptions.Issuer,
                    AuthOptions.Audience,
                    notBefore: now,
                    claims: new List<Claim>
                    {
                        new Claim("id", user.Id.ToString()),
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                        new Claim("surname", user.Surname),
                        new Claim("login", user.Login),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                    },
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                        SecurityAlgorithms.HmacSha256));

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt
                };

                return new JsonResult(response);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> CreateUser([FromBody] RegistrationModel registrationModel)
        {
            try
            {
                var user = new UserDto
                {
                    Name = registrationModel.Name,
                    Surname = registrationModel.Surname,
                    Login = registrationModel.Login,
                    Password = registrationModel.Password,
                    Role = 0
                };
                await _workWithUser.CreateUser(user);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidCastException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}