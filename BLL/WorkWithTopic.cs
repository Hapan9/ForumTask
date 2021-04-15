using System;
using System.Collections.Generic;
using System.Text.Json;
using DAL.Models;
using DAL;


namespace BLL
{
    public class WorkWithTopic
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public void CreateNewTopic(string _name, Guid _userId)
        {
            if (unitOfWork.Users.Get(_userId) == null)
                return;

            unitOfWork.Topics.Create(new Topic() { Name = _name, User_Id = _userId }) ;

        }

        public void UpdateTopic(Guid _id, string _name, Guid _userId)
        {
            if (unitOfWork.Topics.Get(_id) == null || unitOfWork.Users.Get(_userId) == null)
                return;

            unitOfWork.Topics.Update(new Topic() { Id = _id, Name = _name, User_Id = _userId });
        }

        public void DeleteTopic(Guid _id)
        {
            if (unitOfWork.Topics.Get(_id) == null)
                return;

            unitOfWork.Topics.Delete(_id);
        }

        public string GetTopic(Guid _id)
        {
            if (unitOfWork.Topics.Get(_id) == null)
                return null;

            return JsonSerializer.Serialize((Topic)unitOfWork.Topics.Get(_id));
        }

        public string GetAllTopics()
        {
            return JsonSerializer.Serialize((IEnumerable<Topic>)unitOfWork.Topics.GetAll());
        }
    }
}
