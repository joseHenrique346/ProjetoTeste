using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IOrderService
    {
        Task<List<OutputOrder>> GetAll();
        Task<OutputOrder> Get(long id);
        Task<BaseResponse<OutputOrder>> Create(InputCreateOrder input);
        Task<string> GetBrandNameById(long? id);
    }
}