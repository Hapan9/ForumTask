using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.Interfaces;
using DAL.Models;

namespace BLL
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
                throw new ArgumentException("Topic if undefined");
            if (await _unitOfWork.Users.Get(messageDto.UserId) == null)
                throw new ArgumentException("User if undefined");
            if (messageDto.Text.Length < 1)
                throw new ArgumentException("Invalid input");

            var newMessage = _mapper.Map<Message>(messageDto);

            await _unitOfWork.Messages.Create(newMessage);
        }

        public async Task<Message> GetMessage(Guid id)
        {
            if (await _unitOfWork.Messages.Get(id) == null)
                throw new ArgumentException("Message if undefined");

            return await _unitOfWork.Messages.Get(id);
        }

        public async Task<IEnumerable<Message>> GetMessages()
        {
            return await _unitOfWork.Messages.GetAll();
        }

        public async Task UpdateMessage(Guid id, MessageDto messageDto)
        {
            if (await _unitOfWork.Topics.Get(messageDto.TopicId) == null)
                throw new ArgumentException("Topic if undefined");
            if (await _unitOfWork.Users.Get(messageDto.UserId) == null)
                throw new ArgumentException("User if undefined");
            if (messageDto.Text.Length < 1)
                throw new ArgumentException("Invalid input");
            if (await _unitOfWork.Messages.Get(id) == null)
                throw new ArgumentException("Message if undefined");

            var updatedMessage = _mapper.Map<Message>(messageDto);

            updatedMessage.Id = id;

            await _unitOfWork.Messages.Update(updatedMessage);
        }

        public async Task DeleteMessage(Guid id)
        {
            if (await _unitOfWork.Messages.Get(id) == null)
                throw new ArgumentException("Message if undefined");

            await _unitOfWork.Messages.Delete(id);
        }
    }
}