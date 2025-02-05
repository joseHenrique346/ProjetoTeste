using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Api.Controllers
{
    public class CustomerController : BaseController<ICustomerService, Customer, InputIdentityViewCustomer, InputCreateCustomer, InputIdentityUpdateCustomer, InputIdentityDeleteCustomer, OutputCustomer>
    {
        #region Dependency Injection

        private readonly ICustomerService _service;
        public CustomerController(IUnitOfWork unitOfWork, ICustomerService service) : base(unitOfWork, service)
        {
            _service = service;
        }

        #endregion
    }
}