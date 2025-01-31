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

        [HttpPost("GetBySingleId")]
        public async Task<ActionResult<OutputCustomer>> GetSingle(InputIdentityViewCustomer inputIdentityViewCustomer)
        {
            return Ok(await _customerService.GetSingle(inputIdentityViewCustomer));
        }

        [HttpPost("GetByListId")]
        public async Task<ActionResult<OutputCustomer>> Get(List<InputIdentityViewCustomer> listInputIdentityViewCustomer)
        {
            return Ok(await _customerService.Get(listInputIdentityViewCustomer));
        }

        [HttpGet]
        public async Task<ActionResult<OutputCustomer>> GetAll()
        {
            return Ok(await _customerService.GetAll());
        }
        #endregion

        #region Post

        [HttpPost("Single")]
        public async Task<ActionResult<BaseResponse<OutputCustomer>>> CreateSingle(InputCreateCustomer inputCreateCustomer)
        {
            var result = await _customerService.CreateSingle(inputCreateCustomer);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

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

        [HttpPut("Single")]
        public async Task<ActionResult<BaseResponse<OutputCustomer>>> UpdateSingle(InputIdentityUpdateCustomer inputIdentityUpdateCustomer)
        {
            var result = await _customerService.UpdateSingle(inputIdentityUpdateCustomer);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

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

        [HttpDelete("Single")]
        public async Task<ActionResult<BaseResponse<string>>> DeleteSingle(InputIdentityDeleteCustomer inputIdentityDeleteCustomer)
        {
            var result = await _customerService.DeleteSingle(inputIdentityDeleteCustomer);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

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