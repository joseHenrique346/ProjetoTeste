using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface ICustomerValidateService
    {
        Task<Response<InputCreateCustomer>> ValidateCreateCustomer(InputCreateCustomer input);
    }
}