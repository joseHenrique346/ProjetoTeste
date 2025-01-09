using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Repository
{
    public interface IOrderRepository : IRepository<Order> 
    {
        Task<List<Order?>> GetWithIncludesAsync(long id);
        Task<List<Order?>> GetWithIncludesAsync();
    }
}