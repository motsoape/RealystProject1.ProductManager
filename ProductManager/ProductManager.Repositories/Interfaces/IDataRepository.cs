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
        Task AddBulk(IEnumerable<T> entities);
        Task Add(T entity);
        Task Update(int id, T newEntity);
        Task Delete(T entity);
    }
}
