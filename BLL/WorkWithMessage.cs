using System;
using System.Collections.Generic;
using System.Text.Json;
using DAL.Models;
using DAL;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.DTOs;

namespace BLL
{
    public class WorkWithMessage : IWorkWithMessage
    {
        UnitOfWork _unitOfWork;

        public WorkWithMessage(Db db)
        {
            _unitOfWork = new UnitOfWork(db);
        }

        public async Task CreateMessage(MessageDTO messageDTO)
        {
            if (_unitOfWork.Topics.Get(messageDTO.TopicId) == null && _unitOfWork.Users.Get(messageDTO.UserId) == null)
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
            if(_unitOfWork.Messages.Get(id) == null)
                throw new ArgumentException();

            return await _unitOfWork.Messages.Get(id);
        }

        public async Task<IEnumerable<Message>> GetMessages()
        {
            return await _unitOfWork.Messages.GetAll();
        }

        public async Task UpdateMessage(Guid messageId, MessageDTO messageDTO)
        {
            if (_unitOfWork.Topics.Get(messageDTO.TopicId) == null && _unitOfWork.Users.Get(messageDTO.UserId) == null)
                throw new ArgumentException();
            else if (messageDTO.Text.Length < 1)
                throw new ArgumentException();
            else if (_unitOfWork.Messages.Get(messageId) == null)
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
            if (_unitOfWork.Messages.Get(messageId) == null)
                throw new ArgumentException();

            await _unitOfWork.Messages.Delete(messageId);
        }
    }
}
