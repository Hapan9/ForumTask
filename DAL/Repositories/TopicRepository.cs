using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly Db _db;

        public TopicRepository(Db db)
        {
            _db = db;
        }

        public async Task Create(Topic item)
        {
            await _db.Topics.AddAsync(item);
        }

        public async Task Delete(Guid id)
        {
            _db.Messages.RemoveRange(_db.Messages.Where(m => m.TopicId == id));
            _db.Topics.Remove(await _db.Topics.FirstAsync(t => t.Id == id));
        }

        public async Task<Topic> Get(Guid id)
        {
            if (await _db.Topics.CountAsync(t => t.Id == id) == 0)
                return null;

            return await _db.Topics.FirstAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Topic>> GetAll()
        {
            return await _db.Topics.ToListAsync();
        }

        public async Task Update(Topic item)
        {
            _db.Topics.Remove(await _db.Topics.FirstAsync(t => t.Id == item.Id));
            await _db.Topics.AddAsync(item);
        }
    }
}