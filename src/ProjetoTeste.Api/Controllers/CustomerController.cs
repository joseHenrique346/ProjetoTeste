using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Service;

namespace ProjetoTeste.Api.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly CustomerService _customerService;

        public CustomerController(IUnitOfWork unitOfWork, CustomerService customerService) : base(unitOfWork)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<OutputCustomer>> Get(long id)
        {
            var getCustomer = await _customerService.Get(id);
            return Ok(getCustomer.Request);
        }

        [HttpPost]
        public async Task<ActionResult<OutputCustomer>> Create(InputCreateCustomer input)
        {
            var result = await _customerService.Create(input);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Request);
        }
    }
}