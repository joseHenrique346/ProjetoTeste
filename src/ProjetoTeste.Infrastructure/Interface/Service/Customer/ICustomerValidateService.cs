using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface ICustomerValidateService
    {
        Task<BaseResponse<List<CustomerValidate>>> ValidateCreateCustomer(List<CustomerValidate> inputCreate);
        Task<BaseResponse<List<CustomerValidate>>> ValidateUpdateCustomer(List<CustomerValidate> inputUpdate);
        Task<BaseResponse<List<CustomerValidate>>> ValidateDeleteCustomer(List<CustomerValidate> inputDelete);
    }
}