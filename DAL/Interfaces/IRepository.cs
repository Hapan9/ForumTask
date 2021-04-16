using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        Task Create(T item);

        Task Delete(Guid id);

        Task<T> Get(Guid id);

        Task<IEnumerable<T>> GetAll();

        Task Update(T item);
    }
}
