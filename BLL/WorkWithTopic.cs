using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.DTOs;

namespace BLL
{
    public class WorkWithTopic : IWorkWithTopic
    {
        UnitOfWork _unitOfWork;

        public WorkWithTopic(Db db)
        {
            _unitOfWork = new UnitOfWork(db);
        }

        public async Task CreateNewTopic(TopicDTO topicDTO)
        {
            if (_unitOfWork.Users.Get(topicDTO.UserId) == null)
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
            if (_unitOfWork.Topics.Get(id) == null || _unitOfWork.Users.Get(topicDTO.UserId) == null)
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
            if (_unitOfWork.Topics.Get(id) == null)
                throw new ArgumentException();

            await _unitOfWork.Topics.Delete(id);
        }

        public async Task<Topic> GetTopic(Guid id)
        {
            if (_unitOfWork.Topics.Get(id) == null)
                throw new ArgumentException();

            return await Task.Run(() => _unitOfWork.Topics.Get(id));
        }

        public async Task<IEnumerable<Topic>> GetTopics()
        {
            return await Task.Run(() => _unitOfWork.Topics.GetAll());
        }

        public async Task<IEnumerable<Message>> GetMessages(Guid id)
        {
            if (_unitOfWork.Topics.Get(id) == null)
                throw new ArgumentException();

            return await Task.Run(() => _unitOfWork.Messages.GetAll().Where(m => m.TopicId == id).ToList());
        }
    }
}
