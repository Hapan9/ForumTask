using System;
using System.Threading.Tasks;
using BLL;
using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("Login")]
        public async Task<IActionResult> UserAuthorize([FromBody] AuthorizationDto authorizationDto)
        {
            try
            {
                var user = await _workWithUser.CheckUserForm(authorizationDto);

                if (user == null)
                    return NotFound("Wrong password");

                var encodedJwt = JwtGenerator.GenerateJwt(user);

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
                var userDto = new UserDto
                {
                    Name = registrationModel.Name,
                    Surname = registrationModel.Surname,
                    Login = registrationModel.Login,
                    Password = registrationModel.Password,
                    Role = 0
                };
                await _workWithUser.CreateUser(userDto);

                return Ok();
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
    }
}