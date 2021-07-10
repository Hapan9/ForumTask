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

namespace BLL.Services
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

        public async Task CreateTopic(TopicDto topicDto)
        {
            if (await _unitOfWork.Users.Get(topicDto.UserId) == null)
                throw new ArgumentException("User is undefined");

            var newTopic = _mapper.Map<Topic>(topicDto);

            await _unitOfWork.Topics.Create(newTopic);

            await _unitOfWork.Save();
        }

        public async Task UpdateTopic(Guid id, TopicDto topicDto)
        {
            if (await _unitOfWork.Topics.Get(id) == null)
                throw new ArgumentException("Topic is undefined");
            if (await _unitOfWork.Users.Get(topicDto.UserId) == null)
                throw new ArgumentException("User is undefined");

            var updatedTopic = _mapper.Map<Topic>(topicDto);

            updatedTopic.Id = id;

            await _unitOfWork.Topics.Update(updatedTopic);

            await _unitOfWork.Save();
        }

        public async Task DeleteTopic(Guid id)
        {
            if (await _unitOfWork.Topics.Get(id) == null)
                throw new ArgumentException("Topic is undefined");

            await _unitOfWork.Topics.Delete(id);

            await _unitOfWork.Save();
        }

        public async Task<TopicDto> GetTopic(Guid id)
        {
            if (await _unitOfWork.Topics.Get(id) == null)
                throw new ArgumentException("Topic is undefined");

            return _mapper.Map<TopicDto>(await _unitOfWork.Topics.Get(id));
        }

        public async Task<IEnumerable<TopicDto>> GetTopics()
        {
            return _mapper.Map<IEnumerable<TopicDto>>(await _unitOfWork.Topics.GetAll());
        }

        public async Task<IEnumerable<MessageDto>> GetMessages(Guid id)
        {
            if (await _unitOfWork.Topics.Get(id) == null)
                throw new ArgumentException("Topic is undefined");

            var messagesList = (await _unitOfWork.Messages.GetAll()).Where(m => m.TopicId == id).ToList();

            return _mapper.Map<IEnumerable<MessageDto>>(messagesList);
        }
    }
}