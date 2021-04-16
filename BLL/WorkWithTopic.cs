using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL;
using DAL.Interfaces;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.DTOs;

namespace BLL
{
    public class WorkWithTopic : IWorkWithTopic
    {
        IUnitOfWork _unitOfWork;

        public WorkWithTopic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateNewTopic(TopicDTO topicDTO)
        {
            if (await _unitOfWork.Users.Get(topicDTO.UserId) == null)
                throw new ArgumentException();

            var newTopic = new Topic()
            {
                Name = topicDTO.Name,
                UserId = topicDTO.UserId
            };

            await _unitOfWork.Topics.Create(newTopic);

        }

        public async Task UpdateTopic(Guid id, TopicDTO topicDTO)
        {
            if (await _unitOfWork.Topics.Get(id) == null || await _unitOfWork.Users.Get(topicDTO.UserId) == null)
                throw new ArgumentException();

            var UpdatedTopic = new Topic()
            {
                Id = id,
                Name = topicDTO.Name,
                UserId = topicDTO.UserId
            };

            await _unitOfWork.Topics.Update(UpdatedTopic);
        }

        public async Task DeleteTopic(Guid id)
        {
            if (await _unitOfWork.Topics.Get(id) == null)
                throw new ArgumentException();

            await _unitOfWork.Topics.Delete(id);
        }

        public async Task<Topic> GetTopic(Guid id)
        {
            if (await _unitOfWork.Topics.Get(id) == null)
                throw new ArgumentException();

            return await _unitOfWork.Topics.Get(id);
        }

        public async Task<IEnumerable<Topic>> GetTopics()
        {
            return await _unitOfWork.Topics.GetAll();
        }

        public async Task<IEnumerable<Message>> GetMessages(Guid id)
        {
            if (await _unitOfWork.Topics.Get(id) == null)
                throw new ArgumentException();

            var messagesList = (await _unitOfWork.Messages.GetAll()).Where(m => m.TopicId == id).ToList();

            return messagesList;
        }
    }
}
