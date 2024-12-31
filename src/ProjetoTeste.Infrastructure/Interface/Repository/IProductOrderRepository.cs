using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Repository
{
    public interface IProductOrderRepository : IRepository<ProductOrder> 
    {
        Task<ProductOrder?> GetAsync(long id, long id2);
    }
}
