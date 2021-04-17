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
    public class TopicController : ControllerBase
    {
        ITopicService _workWithTopic;

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
                return Problem();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic([FromBody]TopicDTO topicDTO)
        {
            try
            {
                await _workWithTopic.CreateNewTopic(topicDTO);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopic([FromRoute] Guid id)
        {
            try
            {
                return new JsonResult(await _workWithTopic.GetTopic(id));
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
                return new JsonResult(await _workWithTopic.GetMessages(id));
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
        public async Task<IActionResult> UpdateTopic([FromRoute] Guid id, [FromBody]TopicDTO topicDTO)
        {
            try 
            { 
                await _workWithTopic.UpdateTopic(id, topicDTO);
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
        public async Task<IActionResult> DeleteTopic(Guid id)
        {
            try
            { 
                await _workWithTopic.DeleteTopic(id);
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
