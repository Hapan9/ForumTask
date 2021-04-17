using System;
using System.Collections.Generic;
using System.Text.Json;
using DAL.Models;
using DAL;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.DTOs;
using DAL.Interfaces;
using AutoMapper;
using BLL.Mapers;

namespace BLL
{
    public class MessageService : IMessageService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperProfile.InitialazeAutoMapper().CreateMapper();
        }

        public async Task CreateMessage(MessageDTO messageDTO)
        {
            if (await _unitOfWork.Topics.Get(messageDTO.TopicId) == null && await _unitOfWork.Users.Get(messageDTO.UserId) == null)
                throw new ArgumentException();
            else if (messageDTO.Text.Length < 1)
                throw new ArgumentException();

            var newMessage = _mapper.Map<Message>(messageDTO);

            await _unitOfWork.Messages.Create(newMessage);
        }

        public async Task<Message> GetMessage(Guid id)
        {
            if(await _unitOfWork.Messages.Get(id) == null)
                throw new ArgumentException();

            return await _unitOfWork.Messages.Get(id);
        }

        public async Task<IEnumerable<Message>> GetMessages()
        {
            return await _unitOfWork.Messages.GetAll();
        }

        public async Task UpdateMessage(Guid id, MessageDTO messageDTO)
        {
            if (await _unitOfWork.Topics.Get(messageDTO.TopicId) == null && await _unitOfWork.Users.Get(messageDTO.UserId) == null)
                throw new ArgumentException();
            else if (messageDTO.Text.Length < 1)
                throw new ArgumentException();
            else if (await _unitOfWork.Messages.Get(id) == null)
                throw new ArgumentException();

            var updatedMessage = _mapper.Map<Message>(messageDTO);

            updatedMessage.Id = id;

            await _unitOfWork.Messages.Update(updatedMessage);
        }

        public async Task DeleteMessage(Guid id)
        {
            if (await _unitOfWork.Messages.Get(id) == null)
                throw new ArgumentException();

            await _unitOfWork.Messages.Delete(id);
        }
    }
}
