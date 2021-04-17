using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface ITopicService
    {
        Task CreateNewTopic(TopicDto topicDto);

        Task DeleteTopic(Guid id);

        Task<IEnumerable<Topic>> GetTopics();

        Task<Topic> GetTopic(Guid id);

        Task UpdateTopic(Guid id, TopicDto topicDto);

        Task<IEnumerable<Message>> GetMessages(Guid id);
    }
}