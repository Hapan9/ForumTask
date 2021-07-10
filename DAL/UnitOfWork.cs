using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Db _db;

        public UnitOfWork(IUserRepository users, ITopicRepository topics, IMessageRepository messages, Db db)
        {
            Users = users;
            Topics = topics;
            Messages = messages;
            _db = db;
        }

        public IUserRepository Users { get; }

        public ITopicRepository Topics { get; }

        public IMessageRepository Messages { get; }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}