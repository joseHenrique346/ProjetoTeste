using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IOrderService
    {
        Task<List<OutputOrder>> GetAll();
        Task<List<OutputOrder>> Get(List<InputIdentityViewOrder> input);
        Task<BaseResponse<List<OutputOrder>>> Create(List<InputCreateOrder> input);
        Task<BaseResponse<List<OutputProductOrder>>> CreateProductOrder(List<InputCreateProductOrder> input);
    }
}