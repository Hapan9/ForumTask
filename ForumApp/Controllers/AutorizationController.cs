using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTOs;
using System.IdentityModel.Tokens.Jwt;
using BLL;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using PL.Models;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizationController : ControllerBase
    {

        IUserServise _workWithUser;

        public AutorizationController(IUserServise workWithUser)
        {
            _workWithUser = workWithUser;
        }

        [HttpPost]
        public async Task<IActionResult> UserAutorize([FromBody]AutorizationDTO autorizationDTO)
        {
            try
            {
                var user = await _workWithUser.CheckUserForm(autorizationDTO);

                if (user != null)
                {
                    var now = DateTime.UtcNow;

                    var jwt = new JwtSecurityToken(
                            issuer: AuthOptions.ISSUER,
                            audience: AuthOptions.AUDIENCE,
                            notBefore: now,
                            claims: new List<Claim>
                            {
                                new Claim("id", user.Id.ToString()),
                                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                                new Claim("surname", user.Surname),
                                new Claim("login", user.Login),
                                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                            },
                            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                    
                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                    var response = new
                    {
                        access_token = encodedJwt
                    };

                    return new JsonResult(response);
                    //return Ok();
                }
                else
                    return NotFound("Wrong password");
            }
            catch(ArgumentException ex)
            {
                return NotFound();
            }
            catch
            {
                return Problem();
            }
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> CreateUser([FromBody]RegistrationModel registrationModel)
        {
            try
            {
                var user = new UserDTO()
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
                return NotFound();
            }
            catch (InvalidCastException ex)
            {
                return Problem("Alredy registred");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

    }
}
