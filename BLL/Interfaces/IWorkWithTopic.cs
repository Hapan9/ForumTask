using System;
using System.Threading.Tasks;
using BLL.DTOs;
using DAL.Models;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IWorkWithTopic
    {

        Task CreateNewTopic(TopicDTO topicDTO);

        Task DeleteTopic(Guid id);

        Task<IEnumerable<Topic>> GetTopics();

        Task<Topic> GetTopic(Guid id);

        Task UpdateTopic(Guid id, TopicDTO topicDTO);

        Task<IEnumerable<Message>> GetMessages(Guid id);

    }
}