using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;

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

        [HttpGet]
        public async Task<ActionResult<List<OutputProduct>>> GetAll()
        {
            var result = await _productService.GetAll();
            return Ok(result.ToListOutputProduct());
        }

        [HttpPost("GetByListId")]
        public async Task<ActionResult<OutputProduct>> GetId(List<InputIdentityViewProduct> listInputIdentityViewProduct)
        {
            var result = await _productService.Get(listInputIdentityViewProduct);
            return Ok(result);
        }

        #endregion

        #region Post

        [HttpPost]
        public async Task<ActionResult<OutputProduct>> Create(List<InputCreateProduct> listInputCreateProduct)
        {
            var result = await _productService.Create(listInputCreateProduct);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            if (result is null)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Message);
        }

        #endregion

        #region Put

        [HttpPut]
        public async Task<ActionResult<OutputProduct>> Update(List<InputIdentityUpdateProduct> listIdentityUpdateProduct)
        {
            var result = await _productService.Update(listIdentityUpdateProduct);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        #endregion

        #region Delete

        [HttpDelete]
        public async Task<ActionResult<BaseResponse<string>>> Delete(List<InputIdentityDeleteProduct> listInputIdentityDeleteProduct)
        {
            var result = await _productService.Delete(listInputIdentityDeleteProduct);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        #endregion
    }
}