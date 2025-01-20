using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface ICustomerValidateService
    {
        Task<BaseResponse<List<OutputCustomer>>> ValidateCreateCustomer(List<InputCreateCustomer> inputCreate);
        Task<BaseResponse<List<OutputCustomer>>> ValidateUpdateCustomer(List<InputUpdateCustomer> inputUpdate);
        Task<BaseResponse<List<bool>>> ValidateDeleteCustomer(List<long> id);
    }
}