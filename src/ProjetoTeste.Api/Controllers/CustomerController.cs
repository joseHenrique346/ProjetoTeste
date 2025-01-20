using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;

namespace ProjetoTeste.Api.Controllers
{
    public class CustomerController : BaseController
    {
        #region Dependency Injection

        private readonly ICustomerService _customerService;

        public CustomerController(IUnitOfWork unitOfWork, ICustomerService customerService) : base(unitOfWork)
        {
            _customerService = customerService;
        }

        #endregion

        #region Get

        [HttpGet("id")]
        public async Task<ActionResult<OutputCustomer>> Get(long id)
        {
            var getCustomer = await _customerService.Get(id);
            return Ok(getCustomer);
        }

        [HttpGet]
        public async Task<ActionResult<OutputCustomer>> GetAll()
        {
            var getAllCustomers = await _customerService.GetAll();
            return Ok(getAllCustomers);
        }
        #endregion

        #region Post

        [HttpPost]
        public async Task<ActionResult<OutputCustomer>> Create(InputCreateCustomer inputCreate)
        {
            var result = await _customerService.Create(inputCreate);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Content);
        }

        #endregion

        #region Put
        [HttpPut]
        public async Task<ActionResult<OutputCustomer>> Update(InputUpdateCustomer inputUpdate)
        {
            var result = await _customerService.Update(inputUpdate);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Content);
        }

        #endregion

        #region Delete

        [HttpDelete]
        public async Task<ActionResult<string>> Delete(long id)
        {
            var result = await _customerService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }

        #endregion
    }
}