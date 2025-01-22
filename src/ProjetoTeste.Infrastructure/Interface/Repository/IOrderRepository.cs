using ProjetoTeste.Arguments.Arguments.Order.Reports.DTO;
using ProjetoTeste.Arguments.Arguments.Order.Reports.Outputs;
using ProjetoTeste.Infrastructure.Persistence.Entities;
using ProjetoTeste.Infrastructure.Persistence.Repositories;

namespace ProjetoTeste.Infrastructure.Interface.Repository
{
    public interface IOrderRepository : IRepository<Order> 
    {
        Task<List<Order?>> GetWithIncludesAsync(List<long> id);
        Task<List<Order?>> GetWithIncludesAsync();
        Task<List<MaxSaleValueProductDTO>> GetMostOrderedProduct();
        Task<List<MinSaleValueProductDTO>> GetLeastOrderedProduct();

        Task<AverageSaleValueOrderDTO> GetOrderAveragePrice();
        Task<MostOrderedBrandDTO> GetMostOrderedBrand();
        Task<MostOrdersClientDTO?> GetMostOrdersCustomer();
        Task<MostValueOrderClientDTO> GetMostValueOrderCustomer();
    }
}