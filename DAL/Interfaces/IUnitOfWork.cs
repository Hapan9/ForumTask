using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IMessageRepository Messages { get; }
        ITopicRepository Topics { get; }
        IUserRepository Users { get; }

        Task Save();
    }
}