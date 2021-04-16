using System;
using System.Collections.Generic;
using System.Text.Json;
using DAL.Models;
using DAL;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.DTOs;
using DAL.Interfaces;

namespace BLL
{
    public class WorkWithMessage : IWorkWithMessage
    {
        IUnitOfWork _unitOfWork;

        public WorkWithMessage(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateMessage(MessageDTO messageDTO)
        {
            if (await _unitOfWork.Topics.Get(messageDTO.TopicId) == null && await _unitOfWork.Users.Get(messageDTO.UserId) == null)
                throw new ArgumentException();
            else if (messageDTO.Text.Length < 1)
                throw new ArgumentException();

            var newMessage = new Message()
            {
                Text = messageDTO.Text,
                UserId = messageDTO.UserId,
                TopicId = messageDTO.TopicId
            };

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

        public async Task UpdateMessage(Guid messageId, MessageDTO messageDTO)
        {
            if (await _unitOfWork.Topics.Get(messageDTO.TopicId) == null && await _unitOfWork.Users.Get(messageDTO.UserId) == null)
                throw new ArgumentException();
            else if (messageDTO.Text.Length < 1)
                throw new ArgumentException();
            else if (await _unitOfWork.Messages.Get(messageId) == null)
                throw new ArgumentException();

            var updatedMessage = new Message()
            {
                Text = messageDTO.Text,
                UserId = messageDTO.UserId,
                TopicId = messageDTO.TopicId,
                Id = messageId
            };

            await _unitOfWork.Messages.Update(updatedMessage);
        }

        public async Task DeleteMessage(Guid messageId)
        {
            if (await _unitOfWork.Messages.Get(messageId) == null)
                throw new ArgumentException();

            await _unitOfWork.Messages.Delete(messageId);
        }
    }
}
