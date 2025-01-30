using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IOrderValidateService
    {
        Task<BaseResponse<List<OrderValidate?>>> ValidateCreateOrder(List<OrderValidate> input);
        Task<BaseResponse<List<ProductOrderValidate?>>> ValidateCreateProductOrder(List<ProductOrderValidate> input);
    }
}
