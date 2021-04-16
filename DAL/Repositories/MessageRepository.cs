using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Interfaces;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        private readonly Db _db;

        public MessageRepository(Db db)
        {
            _db = db;
        }

        public async Task Create(Message item)
        {
            await Task.Run(() => _db.Messages.AddAsync(item));
            _db.SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            await Task.Run(() => _db.Messages.Remove(_db.Messages.First(m => m.Id == id)));
            _db.SaveChanges();
        }

        public Message Get(Guid id)
        {
            if (_db.Messages.Where(m => m.Id == id).Count() == 0)
                return null;

            return _db.Messages.First(m => m.Id == id);
        }

        public IEnumerable<Message> GetAll()
        {
            return _db.Messages;
        }

        public async Task Update(Message item)
        {
            await Task.Run(() => 
                {
                    _db.Messages.Remove(_db.Messages.First(m => m.Id == item.Id));
                    _db.Messages.Add(item);
                }
            );

            _db.SaveChanges();
        }
    }
}
