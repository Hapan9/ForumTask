using DAL.Interfaces;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Message> Messages { get; }
        IRepository<Topic> Topics { get; }
        IRepository<User> Users { get; }
    }
}