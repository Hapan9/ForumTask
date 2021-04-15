using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;
using DAL.Models;
using System.Linq;

namespace DAL.Repositories
{
    public class TopicRepository : IRepository<ITopic>
    {

        Db _db = new Db();

        public void Create(ITopic _item)
        {
            _db.Topics.AddAsync((Topic)_item);
            _db.SaveChanges();

            _db.Users.Where(u => u.Id == _item.User_Id).First().Topics = _db.Topics.Where(top => _db.Users.Where(u => u.Topics.Where(t => t.Id == top.Id).Count() == 1).Count() == 0).ToList();

            _db.SaveChanges();
        }

        public void Delete(Guid _id)
        {
            _db.Topics.Remove(_db.Topics.Where(t => t.Id == _id).First());
            _db.Messages.RemoveRange(_db.Messages.Where(m => m.Id == _id));
            _db.SaveChanges();
        }

        public ITopic Get(Guid _id)
        {
            if (_db.Topics.Where(t => t.Id == _id).Count() == 0)
                return null;

            ITopic topic = _db.Topics.Where(t => t.Id == _id).First();
            topic.User_Id = _db.Users.Where(u => u.Topics.Where(t => t.Id == topic.Id).Count() == 1).First().Id;

            return topic;
        }

        public IEnumerable<ITopic> GetAll()
        {
            List<Topic> topics = new List<Topic>();
            for(int i = 0; i < _db.Topics.ToList().Count; i++)
            {
                topics.Add(_db.Topics.ToList()[i]);
                topics[topics.Count - 1].User_Id = _db.Users.Where(u => u.Topics.Where(t => t.Id == topics[topics.Count - 1].Id).Count() == 1).First().Id;
            }
            return topics;
        }

        public void Update(ITopic _item)
        {

            Delete(_item.Id);

            Create(_item);

            _db.SaveChanges();
        }
    }
}
