using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface ICustomerService
    {
        Task<List<OutputCustomer>> GetAll();
        Task<BaseResponse<List<OutputCustomer>>> Get(List<InputIdentityViewCustomer> listInputIdentityViewCustomer);
        Task<BaseResponse<List<OutputCustomer>>> Create(List<InputCreateCustomer> listInputCreateCustomer);
        Task<BaseResponse<List<OutputCustomer>>> Update(List<InputIdentityUpdateCustomer> listInputIdentityUpdateCustomer);
        Task<BaseResponse<bool>> Delete(List<InputIdentityDeleteCustomer> listInputIdentityDeleteCustomer);
        Task<OutputCustomer?> GetSingle(InputIdentityViewCustomer inputIdentityViewCustomer);
        Task<BaseResponse<OutputCustomer>> CreateSingle(InputCreateCustomer inputCreateCustomer);
        Task<BaseResponse<OutputCustomer>> UpdateSingle(InputIdentityUpdateCustomer inputIdentityUpdateCustomer);
        Task<BaseResponse<bool>> DeleteSingle(InputIdentityDeleteCustomer inputIdentityDeleteCustomer);
    }
}
