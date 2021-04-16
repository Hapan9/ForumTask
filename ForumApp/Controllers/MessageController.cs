using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.DTOs;

namespace PL.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        IWorkWithMessage _workWithMessage;

        public MessageController(IWorkWithMessage workWithMessage)
        {
            _workWithMessage = workWithMessage;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            try
            {
                return new JsonResult(await Task.Run(() => _workWithMessage.GetMessages().Result));
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

        [HttpPatch("{id}")]
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
                return NotFound();
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }
    }
}
