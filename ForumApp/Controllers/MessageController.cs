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
    public class MessageController : ControllerBase
    {
        WorkWithMessage workWithMessage = new WorkWithMessage();

        [HttpGet("Message")]
        public void CreateMessage([FromBody] MessageModel _message)
        {
            workWithMessage.CreateMessage(_message.Text, _message.UserId, _message.TopicId);
        }

        [HttpPost("Message")]
        public string GetMessages()
        {
            return workWithMessage.GetMessages().Result;
        }

        [HttpPost("Message/{_id}")]
        public string GetMessage([FromRoute]Guid _id)
        {
            return workWithMessage.GetMessage(_id).Result;
        }

        [HttpDelete("Message/{_id}")]
        public void DeleteMessage([FromRoute] Guid _id)
        {
            workWithMessage.DeleteMessage(_id);
        }

        [HttpPatch("Message/{_id}")]
        public void UpdateMessage([FromRoute] Guid _id, [FromBody] MessageModel _message)
        {
            workWithMessage.UpdateMessage(_id, _message.Text, _message.UserId, _message.TopicId);
        }
    }
}
