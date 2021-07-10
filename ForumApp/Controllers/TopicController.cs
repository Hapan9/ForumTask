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
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _workWithTopic;

        public TopicController(ITopicService workWithTopic)
        {
            _workWithTopic = workWithTopic;
        }

        [HttpGet]
        public async Task<IActionResult> GetTopics()
        {
            try
            {
                return new JsonResult(await _workWithTopic.GetTopics());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTopic([FromBody] TopicDto topicDto)
        {
            try
            {
                await _workWithTopic.CreateTopic(topicDto);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopic([FromRoute] Guid id)
        {
            try
            {
                return new JsonResult(await _workWithTopic.GetTopic(id));
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

        [HttpGet("{id}/Messages")]
        public async Task<IActionResult> GetMessages([FromRoute] Guid id)
        {
            try
            {
                return new JsonResult(await _workWithTopic.GetMessages(id));
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

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTopic([FromRoute] Guid id, [FromBody] TopicDto topicDto)
        {
            try
            {
                await _workWithTopic.UpdateTopic(id, topicDto);
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
        public async Task<IActionResult> DeleteTopic(Guid id)
        {
            try
            {
                await _workWithTopic.DeleteTopic(id);
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