using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTeste.Infrastructure.Interface.Repository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T Get(long id);
        Task<List<T>> GetAllAsync();
        Task<T?> GetAsync(long id);
        Task<T> CreateAsync(T entity);
        bool Update(T entity);
        Task<bool> DeleteAsync(long id);
    }
}
