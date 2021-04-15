using System;
using System.Collections.Generic;
using System.Text.Json;
using DAL.Models;
using DAL;
using System.Threading.Tasks;

namespace BLL
{
    public class WorkWithMessage
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public async void CreateMessage(string _text, Guid _userId, Guid _topicId)
        {
            if (unitOfWork.Topics.Get(_topicId) == null && unitOfWork.Users.Get(_userId) == null)
                return;
            else if (_text.Length < 1)
                return;

            await Task.Run(() => unitOfWork.Messages.Create(new Message() { Text = _text, TopicId = _topicId, User_Id = _userId }));
        }

        public async Task<string> GetMessage(Guid _id)
        {
            return await Task.Run(() => JsonSerializer.Serialize((Message)unitOfWork.Messages.Get(_id)));
        }

        public async Task<string> GetMessages()
        {
            return await Task.Run(() => JsonSerializer.Serialize((IEnumerable<Message>)unitOfWork.Messages.GetAll()));
        }

        public async void UpdateMessage(Guid _messageId, string _text, Guid _userId, Guid _topicId)
        {
            if (unitOfWork.Topics.Get(_topicId) == null && unitOfWork.Users.Get(_userId) == null)
                return;
            else if (_text.Length < 1)
                return;
            else if (unitOfWork.Messages.Get(_messageId) == null)
                return;

            await Task.Run(() => unitOfWork.Messages.Update(new Message() { Id = _messageId, Text = _text, TopicId = _topicId, User_Id = _userId }));
        }

        public async void DeleteMessage(Guid _messageId)
        {
            if (unitOfWork.Messages.Get(_messageId) == null)
                return;

            await Task.Run(() => unitOfWork.Messages.Delete(_messageId));
        }
    }
}
