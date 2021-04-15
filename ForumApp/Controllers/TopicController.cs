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
    public class TopicController : ControllerBase
    {
        WorkWithTopic workWithTopic = new WorkWithTopic();

        [HttpGet("Topic")]
        public void CreateTopic([FromBody] TopicModel _topic)
        {
            workWithTopic.CreateNewTopic(_topic.Text, _topic.AuthorId);
        }

        [HttpPatch("Topic/{_id}")]
        public void UpdateTopic([FromRoute] Guid _id, [FromBody]TopicModel _topic)
        {
            workWithTopic.UpdateTopic(_id, _topic.Text, _topic.AuthorId);
        }

        [HttpDelete("Topic/{_id}")]
        public void DeleteTopic(Guid _id)
        {
            workWithTopic.DeleteTopic(_id);
        }

        [HttpPost("Topic/{_id}")]
        public string GetTopic([FromRoute] Guid _id)
        {
            return workWithTopic.GetTopic(_id).Result;
        }

        [HttpPost("Topic")]
        public string GetTopics()
        {
            return workWithTopic.GetAllTopics().Result;
        }
    }
}
