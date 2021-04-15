using System;
using System.Collections.Generic;
using System.Text.Json;
using DAL.Models;
using DAL;

namespace BLL
{
    public class WorkWithMessage
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public void CreateMessage(string _text, Guid _userId, Guid _topicId)
        {
            if (unitOfWork.Topics.Get(_topicId) == null && unitOfWork.Users.Get(_userId) == null)
                return;
            else if (_text.Length < 1)
                return;

            unitOfWork.Messages.Create(new Message() { Text = _text, TopicId = _topicId, User_Id = _userId });
        }

        public string GetMessage(Guid _id)
        {
            return JsonSerializer.Serialize((Message)unitOfWork.Messages.Get(_id));
        }

        public string GetMessages()
        {
            return JsonSerializer.Serialize((IEnumerable<Message>)unitOfWork.Messages.GetAll());
        }

        public void UpdateMessage(Guid _messageId, string _text, Guid _userId, Guid _topicId)
        {
            if (unitOfWork.Topics.Get(_topicId) == null && unitOfWork.Users.Get(_userId) == null)
                return;
            else if (_text.Length < 1)
                return;
            else if (unitOfWork.Messages.Get(_messageId) == null)
                return;

            unitOfWork.Messages.Update(new Message() { Id = _messageId, Text = _text, TopicId = _topicId, User_Id = _userId });
        }

        public void DeleteMessage(Guid _messageId)
        {
            if (unitOfWork.Messages.Get(_messageId) == null)
                return;

            unitOfWork.Messages.Delete(_messageId);
        }
    }
}
