using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperProfile.InitializeAutoMapper().CreateMapper();
        }

        public async Task CreateMessage(MessageDto messageDto)
        {
            if (await _unitOfWork.Topics.Get(messageDto.TopicId) == null)
                throw new ArgumentException("Topic is undefined");
            if (await _unitOfWork.Users.Get(messageDto.UserId) == null)
                throw new ArgumentException("User is undefined");

            var newMessage = _mapper.Map<Message>(messageDto);

            await _unitOfWork.Messages.Create(newMessage);

            await _unitOfWork.Save();
        }

        public async Task<MessageDto> GetMessage(Guid id)
        {
            if (await _unitOfWork.Messages.Get(id) == null)
                throw new ArgumentException("Message is undefined");

            return _mapper.Map<MessageDto>(await _unitOfWork.Messages.Get(id));
        }

        public async Task<IEnumerable<MessageDto>> GetMessages()
        {
            return _mapper.Map<IEnumerable<MessageDto>>(await _unitOfWork.Messages.GetAll());
        }

        public async Task UpdateMessage(Guid id, MessageDto messageDto)
        {
            if (await _unitOfWork.Topics.Get(messageDto.TopicId) == null)
                throw new ArgumentException("Topic is undefined");
            if (await _unitOfWork.Users.Get(messageDto.UserId) == null)
                throw new ArgumentException("User is undefined");
            if (await _unitOfWork.Messages.Get(id) == null)
                throw new ArgumentException("Message is undefined");

            var updatedMessage = _mapper.Map<Message>(messageDto);

            updatedMessage.Id = id;

            await _unitOfWork.Messages.Update(updatedMessage);

            await _unitOfWork.Save();
        }

        public async Task DeleteMessage(Guid id)
        {
            if (await _unitOfWork.Messages.Get(id) == null)
                throw new ArgumentException("Message is undefined");

            await _unitOfWork.Messages.Delete(id);

            await _unitOfWork.Save();
        }
    }
}