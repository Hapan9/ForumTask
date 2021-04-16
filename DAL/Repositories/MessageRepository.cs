using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            await _db.Messages.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _db.Messages.Remove(await _db.Messages.FirstAsync(m => m.Id == id));
            await _db.SaveChangesAsync();
        }

        public async Task<Message> Get(Guid id)
        {
            if (await _db.Messages.CountAsync(m => m.Id == id) == 0)
                return null;

            return await _db.Messages.FirstAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            return await _db.Messages.ToListAsync();
        }

        public async Task Update(Message item)
        {
            _db.Messages.Remove(await _db.Messages.FirstAsync(m => m.Id == item.Id));
            await _db.Messages.AddAsync(item);

            await _db.SaveChangesAsync();
        }
    }
}
