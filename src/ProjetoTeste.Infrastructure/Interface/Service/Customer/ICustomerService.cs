using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAll();
        Task<BaseResponse<Customer>> Get(long id);
        Task<BaseResponse<Customer>> Create(InputCreateCustomer product);
    }
}
