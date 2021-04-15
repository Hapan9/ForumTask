using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        Db _db = new Db();

        public void Create(IUser _item)
        {
            if (_db.Users.Where(u => u.Login == _item.Login).Count() > 0)
                return;
            _db.Users.AddAsync((User)_item);
            _db.SaveChanges();
        }

        public void Delete(Guid _id)
        {
            

            IUser user = _db.Users.Where(u => u.Id == _id).FirstOrDefault();

            MessageRepository messageRepository = new MessageRepository();
            TopicRepository topicRepository = new TopicRepository();

            _db.Messages.RemoveRange(((IEnumerable<Message>)messageRepository.GetAll()).Where(m => m.User_Id == user.Id));
            _db.SaveChanges();
            for (int i = 0; i <  topicRepository.GetAll().Where(t => t.User_Id == user.Id).ToList().Count; i++)
            {
                topicRepository.Delete(topicRepository.GetAll().Where(t => t.User_Id == user.Id).ToList()[i].Id);
            }
            _db.SaveChanges();

            _db.Users.Remove(_db.Users.Where(u => u.Id == _id).First());
            _db.SaveChanges();
        }

        public IUser Get(Guid _id)
        {
            if (_db.Users.Where(u => u.Id == _id).Count() == 0)
                return null;

            return _db.Users.Where(u => u.Id == _id).First();
        }

        public IEnumerable<IUser> GetAll()
        {
            return _db.Users;
        }

        public void Update(IUser _item)
        {
            Delete(_item.Id);

            Create(_item);

            _db.SaveChanges();
        }
    }
}
