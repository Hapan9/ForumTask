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
    public class MessageController : ControllerBase
    {
        IMessageService _workWithMessage;

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
                return Problem();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage([FromRoute]Guid id)
        {
            try
            {
                return new JsonResult(await _workWithMessage.GetMessage(id));
            }
            catch(ArgumentException ex)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return Problem();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] MessageDTO messageDTO)
        {
            try
            {
                await _workWithMessage.CreateMessage(messageDTO);
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

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage([FromRoute] Guid id, [FromBody] MessageDTO messageDTO)
        {
            try
            {
                await _workWithMessage.UpdateMessage(id, messageDTO);
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

        [Authorize(Roles = "Administrator, Moderator")]
        public async Task<IActionResult> DeleteMessage([FromRoute] Guid id)
        {

            try
            {
                await _workWithMessage.DeleteMessage(id);
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
