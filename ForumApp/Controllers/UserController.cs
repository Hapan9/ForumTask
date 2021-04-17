using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserServise _workWithUser;

        public UserController(IUserServise workWithUser)
        {
            _workWithUser = workWithUser;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return new JsonResult(await _workWithUser.GetUsers());
            }
            catch (Exception ex)
            {
                return Problem();
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            try { 
                return new JsonResult(await _workWithUser.GetUser(id));
            }
            catch (ArgumentException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }

        [HttpGet("{id}/Topics")]
        public async Task<IActionResult> GetTopics([FromRoute] Guid id)
        {
            try
            {
                return new JsonResult(await _workWithUser.GetTopics(id));
            }
            catch (ArgumentException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }

        [HttpGet("{id}/Messages")]
        public async Task<IActionResult> GetMessages([FromRoute] Guid id)
        {
            try
            {
                return new JsonResult(await _workWithUser.GetMessages(id));
            }
            catch (ArgumentException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]UserDTO userDTO)
        {
            try {
                await _workWithUser.CreateUser(userDTO);
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

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody]UserDTO userDTO)
        {
            try 
            { 
                await _workWithUser.UpadteUser(id, userDTO);
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
                return Problem();
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            try 
            { 
            await _workWithUser.DeleteUser(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }
    }
}
