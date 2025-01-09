using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAll();
        Task<Response<Customer>> Get(long id);
        Task<Response<Customer>> Create(InputCreateCustomer product);
    }
}
