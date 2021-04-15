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
            return workWithUser.CheckUserEnter(_user_info.Login, _user_info.Password).Result;
        }

        [HttpPost("User/{_id}")]
        public string GetUser([FromRoute]Guid _id)
        {
            return workWithUser.GetUser(_id).Result;
        }

        [HttpPost("User")]
        public string GetUsers()
        {
            return workWithUser.GetUsers().Result;
        }

        [HttpPatch("User/{_id}/{_role}")]
        public void GetUsers([FromRoute] Guid _id, int _role, [FromBody] UserInfoModel _user)
        {
            workWithUser.UpadteUser(_id, _user.Name, _user.Surname, _user.Login, _user.Password, _role);
        }

        [HttpDelete("User/{_id}")]
        public void DeleteUser([FromRoute] Guid _id)
        {
            workWithUser.DeleteUser(_id);
        }
    }
}
