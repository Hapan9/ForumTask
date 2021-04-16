using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly Db _db;

        public UserRepository(Db db)
        {
            _db = db;
        }

        public async Task Create(User item)
        {
            await Task.Run(() =>_db.Users.AddAsync(item));
            _db.SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            await Task.Run(() => 
            {
                _db.Messages.RemoveRange(_db.Messages.Where(m => m.UserId == id));

                foreach (Topic topic in _db.Topics.Where(t => t.UserId == id).ToList())
                    _db.Messages.RemoveRange(_db.Messages.Where(m => m.TopicId == topic.Id));

                _db.Users.Remove(_db.Users.First(u => u.Id == id));
            });
            _db.SaveChanges();
        }

        public User Get(Guid id)
        {
            if (_db.Users.Count(u => u.Id == id) == 0)
                return null;

            return _db.Users.First(u => u.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }

        public async Task Update(User item)
        {
            await Task.Run(() =>
                {
                    _db.Remove(_db.Users.First(u => u.Id == item.Id));
                    _db.Add(item);
                }
            );

            _db.SaveChanges();
        }
    }
}
