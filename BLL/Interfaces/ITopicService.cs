using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface ITopicService
    {
        Task CreateTopic(TopicDto topicDto);

        Task DeleteTopic(Guid id);

        Task<IEnumerable<TopicDto>> GetTopics();

        Task<TopicDto> GetTopic(Guid id);

        Task UpdateTopic(Guid id, TopicDto topicDto);

        Task<IEnumerable<MessageDto>> GetMessages(Guid id);
    }
}