using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IOrderService
    {
        Task<List<Order?>> GetAll();
        Task<Response<Order?>> Get(long id);
        Task<Response<Order?>> Create(InputCreateOrder input);
    }
}