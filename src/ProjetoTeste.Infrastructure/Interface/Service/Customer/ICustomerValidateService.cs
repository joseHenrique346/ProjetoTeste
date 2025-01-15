using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface ICustomerValidateService
    {
        Task<BaseResponse<InputCreateCustomer>> ValidateCreateCustomer(InputCreateCustomer inputCreate);
        Task<BaseResponse<InputUpdateCustomer>> ValidateUpdateCustomer(InputUpdateCustomer inputUpdate);
        Task<BaseResponse<bool>> ValidateDeleteCustomer(long id);
    }
}