using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

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
            await _db.Users.AddAsync(item);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _db.Messages.RemoveRange(_db.Messages.Where(m => m.UserId == id));

            foreach (Topic topic in await _db.Topics.Where(t => t.UserId == id).ToListAsync())
                _db.Messages.RemoveRange(_db.Messages.Where(m => m.TopicId == topic.Id));

            _db.Users.Remove(await _db.Users.FirstAsync(u => u.Id == id));

            await _db.SaveChangesAsync();
        }

        public async Task<User> Get(Guid id)
        {
            if (await _db.Users.CountAsync(u => u.Id == id) == 0)
                return null;

            return await _db.Users.FirstAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task Update(User item)
        {
            _db.Remove(await _db.Users.FirstAsync(u => u.Id == item.Id));
            await _db.AddAsync(item);

            await _db.SaveChangesAsync();
        }
    }
}
