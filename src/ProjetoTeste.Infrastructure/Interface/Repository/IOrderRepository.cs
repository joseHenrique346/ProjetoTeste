using ProjetoTeste.Arguments.Arguments.Order.GetLINQ;
using ProjetoTeste.Infrastructure.Persistence.Entities;
using ProjetoTeste.Infrastructure.Persistence.Repositories;

namespace ProjetoTeste.Infrastructure.Interface.Repository
{
    public interface IOrderRepository : IRepository<Order> 
    {
        Task<List<Order?>> GetWithIncludesAsync(long id);
        Task<List<Order?>> GetWithIncludesAsync();
        Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct();
        Task<List<OutputMinSaleValueProduct>> GetLeastOrderedProduct();

        Task<OutputAverageSaleValueOrder> GetOrderAveragePrice();
        Task<OutputMaxSaleValueBrand?> GetMostOrderedBrand();
        //Task<List<Order?>> GetMostOrderedClient();
        //Task<List<Order?>> GetLeastOrderedClient();
    }
}