using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class MessageRepository : IRepository<IMessage>
    {

        Db _db = new Db();

        public void Create(IMessage _item)
        {
            _db.Messages.AddAsync((Message)_item);
            _db.SaveChanges();

            _db.Users.Where(u => u.Id == _item.User_Id).First().Messages = _db.Messages.Where(mes => _db.Users.Where(u => u.Messages.Where(m => m.Id == mes.Id).Count() == 1).Count() == 0).ToList();
            _db.SaveChanges();
        }

        public void Delete(Guid _id)
        {
            _db.Messages.Remove(_db.Messages.Where(m => m.Id == _id).First());
            _db.SaveChanges();
        }

        public IMessage Get(Guid _id)
        {
            if (_db.Messages.Where(m => m.Id == _id).Count() == 0)
                return null;

            IMessage message = _db.Messages.Where(m => m.Id == _id).First();
            message.User_Id = _db.Users.Where(u => u.Messages.Where(m => m.Id == message.Id).Count() == 1).First().Id;
            return message;
        }

        public IEnumerable<IMessage> GetAll()
        {
            List<Message> messages = new List<Message>();

            for(int i =0; i<_db.Messages.ToList().Count; i++)
            {
                messages.Add(_db.Messages.ToList()[i]);
                messages[messages.Count - 1].User_Id = _db.Users.Where(u => u.Messages.Where(m => m.Id == messages[messages.Count - 1].Id).Count() == 1).First().Id;
            }

            return messages;
        }

        public void Update(IMessage _item)
        {
            Delete(_item.Id);

            Create(_item);

            _db.SaveChanges();
        }
    }
}
