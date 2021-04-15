using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Guid _id);
        void Create(T _item);
        void Update(T _item);
        void Delete(Guid _id);
    }
}
