using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IOrderValidateService
    {
        Task<Response<InputCreateOrder?>> ValidateCreateOrder(InputCreateOrder input);
        Task<Response<InputCreateProductOrder?>> ValidateCreateProductOrder(InputCreateProductOrder input);
    }
}
