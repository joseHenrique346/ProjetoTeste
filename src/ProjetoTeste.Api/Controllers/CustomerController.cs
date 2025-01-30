using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;
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

        [HttpPost("GetByListId")]
        public async Task<ActionResult<OutputCustomer>> Get(List<InputIdentityViewCustomer> listInputIdentityViewCustomer)
        {
            var getCustomer = await _customerService.Get(listInputIdentityViewCustomer);
            return Ok(getCustomer);
        }

        [HttpGet("Multiple")]
        public async Task<ActionResult<OutputCustomer>> GetAll()
        {
            var getAllCustomers = await _customerService.GetAll();
            return Ok(getAllCustomers);
        }
        #endregion

        #region Post

        [HttpPost("Multiple")]
        public async Task<ActionResult<BaseResponse<List<OutputCustomer>>>> Create(List<InputCreateCustomer> listInputCreateCustomer)
        {
            var result = await _customerService.Create(listInputCreateCustomer);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        #endregion

        #region Put
        [HttpPut("Multiple")]
        public async Task<ActionResult<BaseResponse<List<OutputCustomer>>>> Update(List<InputIdentityUpdateCustomer> listInputIdentityUpdateCustomer)
        {
            var result = await _customerService.Update(listInputIdentityUpdateCustomer);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        #endregion

        #region Delete

        [HttpDelete("Multiple")]
        public async Task<ActionResult<BaseResponse<List<string>>>> Delete(List<InputIdentityDeleteCustomer> listInputIdentityDeleteCustomer)
        {
            var result = await _customerService.Delete(listInputIdentityDeleteCustomer);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}

#endregion