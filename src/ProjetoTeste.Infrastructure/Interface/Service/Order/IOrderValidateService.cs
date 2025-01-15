using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IOrderValidateService
    {
        Task<BaseResponse<InputCreateOrder?>> ValidateCreateOrder(InputCreateOrder input);
        Task<BaseResponse<InputCreateProductOrder?>> ValidateCreateProductOrder(InputCreateProductOrder input);
    }
}
