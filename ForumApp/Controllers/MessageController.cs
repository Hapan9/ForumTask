using System;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _workWithMessage;

        public MessageController(IMessageService workWithMessage)
        {
            _workWithMessage = workWithMessage;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            try
            {
                return new JsonResult(await _workWithMessage.GetMessages());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage([FromRoute] Guid id)
        {
            try
            {
                return new JsonResult(await _workWithMessage.GetMessage(id));
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] MessageDto messageDto)
        {
            try
            {
                await _workWithMessage.CreateMessage(messageDto);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage([FromRoute] Guid id, [FromBody] MessageDto messageDto)
        {
            try
            {
                await _workWithMessage.UpdateMessage(id, messageDto);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] Guid id)
        {
            try
            {
                await _workWithMessage.DeleteMessage(id);
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