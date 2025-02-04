using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;

namespace ProjetoTeste.Api.Controllers
{
    public class ProductController : BaseController
    {
        #region Dependency Injection

        private readonly IProductService _productService;
        public ProductController(IUnitOfWork unitOfWork, IProductService productService) : base(unitOfWork)
        {
            _productService = productService;
        }

        #endregion

        #region Get

        [HttpPost("GetBySingleId")]
        public async Task<ActionResult<OutputProduct>> GetId(InputIdentityViewProduct inputIdentityViewProduct)
        {
            return Ok(await _productService.GetSingle(inputIdentityViewProduct));
        }

        [HttpGet]
        public async Task<ActionResult<List<OutputProduct>>> GetAll()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpPost("GetByListId")]
        public async Task<ActionResult<OutputProduct>> GetId(List<InputIdentityViewProduct> listInputIdentityViewProduct)
        {
            return Ok(await _productService.Get(listInputIdentityViewProduct));
        }

        #endregion

        #region Post

        [HttpPost("Single")]
        public async Task<ActionResult<OutputProduct>> CreateSingle(InputCreateProduct inputCreateProduct)
        {
            var result = await _productService.CreateSingle(inputCreateProduct);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("Multiple")]
        public async Task<ActionResult<OutputProduct>> Create(List<InputCreateProduct> listInputCreateProduct)
        {
            var result = await _productService.Create(listInputCreateProduct);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        #endregion

        #region Put

        [HttpPut("Single")]
        public async Task<ActionResult<BaseResponse<OutputProduct>>> UpdateSingle(InputIdentityUpdateProduct inputIdentityUpdateProduct)
        {
            var result = await _productService.UpdateSingle(inputIdentityUpdateProduct);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("Multiple")]
        public async Task<ActionResult<BaseResponse<List<OutputProduct>>>> Update(List<InputIdentityUpdateProduct> listIdentityUpdateProduct)
        {
            var result = await _productService.Update(listIdentityUpdateProduct);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        #endregion

        #region Delete

        [HttpDelete("Single")]
        public async Task<ActionResult<BaseResponse<string>>> DeleteSingle(InputIdentityDeleteProduct inputIdentityDeleteProduct)
        {
            var result = await _productService.DeleteSingle(inputIdentityDeleteProduct);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("Multiple")]
        public async Task<ActionResult<BaseResponse<string>>> Delete(List<InputIdentityDeleteProduct> listInputIdentityDeleteProduct)
        {
            var result = await _productService.Delete(listInputIdentityDeleteProduct);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}

#endregion