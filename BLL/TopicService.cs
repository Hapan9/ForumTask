using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.Interfaces;
using DAL.Models;

namespace BLL
{
    public class TopicService : ITopicService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TopicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperProfile.InitializeAutoMapper().CreateMapper();
        }

        public async Task CreateNewTopic(TopicDto topicDto)
        {
            if (await _unitOfWork.Users.Get(topicDto.UserId) == null)
                throw new ArgumentException("User if undefined");
            if (topicDto.Name.Length < 1)
                throw new ArgumentException("Invalid input");

            var newTopic = _mapper.Map<Topic>(topicDto);

            await _unitOfWork.Topics.Create(newTopic);
        }

        public async Task UpdateTopic(Guid id, TopicDto topicDto)
        {
            if (await _unitOfWork.Topics.Get(id) == null)
                throw new ArgumentException("Topic if undefined");
            if (await _unitOfWork.Users.Get(topicDto.UserId) == null)
                throw new ArgumentException("User if undefined");
            if (topicDto.Name.Length < 1)
                throw new ArgumentException("Invalid input");

            var updatedTopic = _mapper.Map<Topic>(topicDto);

            updatedTopic.Id = id;

            await _unitOfWork.Topics.Update(updatedTopic);
        }

        public async Task DeleteTopic(Guid id)
        {
            if (await _unitOfWork.Topics.Get(id) == null)
                throw new ArgumentException("Topic if undefined");

            await _unitOfWork.Topics.Delete(id);
        }

        public async Task<Topic> GetTopic(Guid id)
        {
            if (await _unitOfWork.Topics.Get(id) == null)
                throw new ArgumentException("Topic if undefined");

            return await _unitOfWork.Topics.Get(id);
        }

        public async Task<IEnumerable<Topic>> GetTopics()
        {
            return await _unitOfWork.Topics.GetAll();
        }

        public async Task<IEnumerable<Message>> GetMessages(Guid id)
        {
            if (await _unitOfWork.Topics.Get(id) == null)
                throw new ArgumentException("Topic if undefined");

            var messagesList = (await _unitOfWork.Messages.GetAll()).Where(m => m.TopicId == id).ToList();

            return messagesList;
        }
    }
}