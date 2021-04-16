using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;
using DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TopicRepository : IRepository<Topic>
    {
        private readonly Db _db;

        public TopicRepository(Db db)
        {
            _db = db;
        }

        public async Task Create(Topic item)
        {
            await Task.Run(() => _db.Topics.Add(item));
            _db.SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            await Task.Run(() =>
                {
                    _db.Messages.RemoveRange(_db.Messages.Where(m => m.TopicId == id));
                    _db.Topics.Remove(_db.Topics.First(t => t.Id == id));
                }
            );
            _db.SaveChanges();
        }

        public Topic Get(Guid id)
        {
            if (_db.Topics.Where(t => t.Id == id).Count() == 0)
                return null;

            return _db.Topics.First(t => t.Id == id);
        }

        public IEnumerable<Topic> GetAll()
        {
            return _db.Topics;
        }

        public async Task Update(Topic item)
        {

            await Task.Run(() =>
                {
                    _db.Topics.Remove(_db.Topics.First(t => t.Id == item.Id));
                    _db.Topics.Add(item);
                }
            );

            _db.SaveChanges();
        }
    }
}
