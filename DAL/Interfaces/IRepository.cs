using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    interface IRepository<T>
    {
        Task Create(T item);

        Task Delete(Guid id);

        T Get(Guid id);

        IEnumerable<T> GetAll();

        Task Update(T item);
    }
}
