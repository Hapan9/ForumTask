using DAL.Interfaces;
using DAL.Models;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IRepository<User> users, IRepository<Topic> topics, IRepository<Message> messages)
        {
            Users = users;
            Topics = topics;
            Messages = messages;
        }

        public IRepository<User> Users { get; }

        public IRepository<Topic> Topics { get; }

        public IRepository<Message> Messages { get; }
    }
}