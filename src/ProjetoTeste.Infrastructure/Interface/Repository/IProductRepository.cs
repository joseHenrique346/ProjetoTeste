using ProjetoTeste.Infrastructure.Persistence.Entities;
using System.Linq.Expressions;

namespace ProjetoTeste.Infrastructure.Interface.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetWithIncludesAsync(long id, params Expression<Func<Product, object>>[] includes);
        Task<Product?> GetByCode(string code);
    }
}