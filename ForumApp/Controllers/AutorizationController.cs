using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTOs;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizationController : ControllerBase
    {

        IWorkWithUser _workWithUser;

        public AutorizationController(IWorkWithUser workWithUser)
        {
            _workWithUser = workWithUser;
        }

        [HttpPost]
        public async Task<IActionResult> UserAutorize([FromBody]AutorizationDTO autorizationDTO)
        {
            try
            {
                if (await _workWithUser.CheckUserForm(autorizationDTO))
                {
                    return Ok();
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
    }
}
