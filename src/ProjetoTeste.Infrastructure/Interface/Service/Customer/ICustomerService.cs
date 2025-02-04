using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Infrastructure.Interface.Base;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface ICustomerService : IBaseService<InputIdentityViewCustomer, InputCreateCustomer, InputIdentityUpdateCustomer, InputIdentityDeleteCustomer, OutputCustomer> { }
}
