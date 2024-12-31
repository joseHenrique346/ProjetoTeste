using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;
using ProjetoTeste.Infrastructure.Service;
using ProjetoTeste.Infrastructure.Conversor;

namespace ProjetoTeste.Api.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ProductService _productService;
        public ProductController(IUnitOfWork unitOfWork, ProductService productService) : base(unitOfWork)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<OutputProduct>>> GetAll()
        {
            var result = await _productService.GetAll();
            return Ok(result.ToListOutputProduct());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetId(long id)
        {
            var result = await _productService.Get(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            if (result == null)
            {
                return NotFound(result.Message);
            }

            var getProduct = result.Request;
            return Ok(getProduct.ToOutputProduct());
        }

        //[HttpGet]
        //public async Task<Product?> GetProductWithBrandAsync(long id)
        //{
        //    return await _productService.GetWithIncludesAsync(id, p => p.Brand);
        //}

        [HttpPost]
        public async Task<ActionResult<OutputProduct>> Create(InputCreateProduct input)
        {
            var result = await _productService.Create(input);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            if (result is null)
            {
                return NotFound(result.Message);
            }

            var createdProduct = result.Request;
            return CreatedAtAction(nameof(Create), createdProduct.ToOutputProduct());
        }

        [HttpPut]
        public async Task<ActionResult<OutputProduct>> Update(int id, InputUpdateProduct input)
        {
            var result = await _productService.Update(id, input);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            if (result is null)
            {
                return NotFound(result.Message);
            }

            var updatedProduct = result.Request;
            return Ok(updatedProduct.ToOutputProduct());
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _productService.Delete(id);
            return Ok(result);
        }
    }
}