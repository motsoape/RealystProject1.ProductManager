using ProductManager.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Repositories.Interfaces
{
    public interface IDataRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        void AddBulk(IEnumerable<T> entities);
        void Add(T entity);
        void Update(T oldEntity, T newEntity);
        void Delete(T entity);
    }
}
