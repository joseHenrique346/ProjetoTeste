using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;
using ProjetoTeste.Infrastructure.Service;


namespace ProjetoTeste.Api.Controllers
{
    [Route("produtos")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly IUnitOfWork _uof;
        public ProductController(IUnitOfWork uof, ProductService productService)
        {
            _productService = productService;
            _uof = uof;
        }

        [Authorize]
        [HttpGet("Busca de Produtos")]
        public async Task<ActionResult<List<OutputProduct>>> GetAll()
        {
            var result = await _uof.ProductRepository.GetAllAsync();
            return Ok(result.ToListOutputProduct());
        }

        [Authorize]
        [HttpGet("Busca Por Id")]
        public async Task<ActionResult<Product>> GetId(long id)
        {
            var product = await _productService.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product.ToOutputProduct());
        }

        //[HttpGet]
        //public async Task<Product?> GetProductWithBrandAsync(long id)
        //{
        //    return await _uof.ProductRepository.GetWithIncludesAsync(id, p => p.Brand);
        //}

        [HttpPost("Criação de Produto")]
        public async Task<ActionResult<OutputProduct>> Create(InputCreateProduct input)
        {
            var product = await _productService.CreateProductAsync(input);
            return CreatedAtAction(nameof(Create), new { id = product.Id }, product.ToOutputProduct());
        }

        [HttpPut("Atualização de Produto")]
        public ActionResult<OutputProduct> Update(int id, InputUpdateProduct input)
        {
            var result = _productService.UpdateProduct(id, input);
            return Ok(result.ToOutputProduct());
        }

        [HttpDelete("Removendo Produto")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            return Ok(result);
        }
    }
}