using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PL.Models;
using BLL;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizationController : ControllerBase
    {
        WorkWithUser workWithUser = new WorkWithUser();

        [HttpGet("User")]
        public void Register([FromBody]UserInfoModel _user)
        {
            workWithUser.UserRegustration(_user.Name, _user.Surname, _user.Login, _user.Password);

        }

        [HttpPost("Authorize")]
        public string Authorize([FromBody]UserAuthorizeModel _user_info)
        {
            return workWithUser.CheckUserEnter(_user_info.Login, _user_info.Password);
        }

        [HttpPost("User/{_id}")]
        public string GetUser([FromRoute]Guid id)
        {
            return workWithUser.GetUser(id);
        }

        [HttpPost("User")]
        public string GetUsers()
        {
            return workWithUser.GetUsers();
        }

        [HttpPatch("User/{_id}")]
        public void GetUsers([FromRoute] Guid id, int role, [FromBody] UserInfoModel _user)
        {
            workWithUser.UpadteUser(id, _user.Name, _user.Surname, _user.Login, _user.Password, role);
        }

        [HttpDelete("User/{_id}")]
        public void DeleteUser([FromRoute] Guid id)
        {
            workWithUser.DeleteUser(id);
        }
    }
}
