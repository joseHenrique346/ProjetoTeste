using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;
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
        public async Task<Response<Customer?>> Get(long id)
        {
            return await _customerService.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Response<Customer>>> Create(InputCreateCustomer input)
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