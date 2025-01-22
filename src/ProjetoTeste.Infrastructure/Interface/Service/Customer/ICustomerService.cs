using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface ICustomerService
    {
        Task<List<OutputCustomer>> GetAll();
        Task<List<OutputCustomer>> Get(List<InputIdentityViewCustomer> input);
        Task<BaseResponse<List<OutputCustomer>>> Create(List<InputCreateCustomer> input);
    }
}
