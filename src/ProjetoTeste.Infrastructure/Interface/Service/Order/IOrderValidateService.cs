using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IOrderValidateService
    {
        Task<BaseResponse<List<OutputOrder?>>> ValidateCreateOrder(List<InputCreateOrder> input);
        Task<BaseResponse<List<OutputProductOrder?>>> ValidateCreateProductOrder(List<InputCreateProductOrder> input);
    }
}
