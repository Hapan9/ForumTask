using System;
using System.Collections.Generic;
using System.Text.Json;
using DAL.Models;
using DAL;
using System.Threading.Tasks;

namespace BLL
{
    public class WorkWithTopic
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public async void CreateNewTopic(string _name, Guid _userId)
        {
            if (unitOfWork.Users.Get(_userId) == null)
                return;

            await Task.Run(() => unitOfWork.Topics.Create(new Topic() { Name = _name, User_Id = _userId }));

        }

        public async void UpdateTopic(Guid _id, string _name, Guid _userId)
        {
            if (unitOfWork.Topics.Get(_id) == null || unitOfWork.Users.Get(_userId) == null)
                return;

            await Task.Run(() => unitOfWork.Topics.Update(new Topic() { Id = _id, Name = _name, User_Id = _userId }));
        }

        public async void DeleteTopic(Guid _id)
        {
            if (unitOfWork.Topics.Get(_id) == null)
                return;

            await Task.Run(() => unitOfWork.Topics.Delete(_id));
        }

        public async Task<string> GetTopic(Guid _id)
        {
            if (unitOfWork.Topics.Get(_id) == null)
                return null;

            return await Task.Run(() => JsonSerializer.Serialize((Topic)unitOfWork.Topics.Get(_id)));
        }

        public async Task<string> GetAllTopics()
        {
            return await Task.Run(() => JsonSerializer.Serialize((IEnumerable<Topic>)unitOfWork.Topics.GetAll()));
        }
    }
}
