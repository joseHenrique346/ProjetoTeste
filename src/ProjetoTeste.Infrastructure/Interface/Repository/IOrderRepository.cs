using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Infrastructure.Persistence.Entities;
using ProjetoTeste.Infrastructure.Persistence.Repositories;

namespace ProjetoTeste.Infrastructure.Interface.Repository
{
    public interface IOrderRepository : IRepository<Order> 
    {
        Task<List<Order?>> GetWithIncludesAsync(long id);
        Task<List<Order?>> GetWithIncludesAsync();
        Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct();
        //Task<Order> GetOrderAveragePrice();
        //Task<List<Order?>> LowerOrderProduct();
        //Task<List<Order?>> GetHigherOrderBrand();
        //Task<List<Order?>> GetHigherOrderClient();
        //Task<List<Order?>> GetLowerOrderClient();
    }
}