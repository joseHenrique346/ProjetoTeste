using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Service;
using ProjetoTeste.Infrastructure.Conversor;

namespace ProjetoTeste.Api.Controllers
{
    [Route("brand")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class BrandController : ControllerBase
    {
        private readonly BrandService _brandService;
        private readonly IUnitOfWork _uof;

        public BrandController(IUnitOfWork uof, BrandService brandService)
        {
            _brandService = brandService;
            _uof = uof;
        }

        [Authorize]
        [HttpGet("GetBrands")]
        public async Task<ActionResult<List<OutputBrand>>> GetAll()
        {
            var brands = await _brandService.GetAllBrandAsync();
            return Ok(brands.ToListOutputBrand());
        }

        [Authorize]
        [HttpGet("GetBrandById")]
        public async Task<ActionResult<OutputBrand>> Get(int id)
        {
            var result = await _brandService.GetBrandAsync(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var brand = result.Request;
            if (brand is null)
            {
                return NotFound(result.Message);
            }

            return Ok(brand.ToOutputBrand());
        }


        [HttpPost("BrandCreation")]
        public async Task<ActionResult<OutputBrand>> CreateAsync(InputCreateBrand input)
        {
            var brand = await _brandService.CreateBrandAsync(input);

            if (brand is null)
            {
                return NotFound(brand.Message);
            }

            if (!brand.Success)
            {
                return BadRequest(brand.Message);
            }

            var newBrand = brand.Request;
            return CreatedAtAction(nameof(Get), newBrand.ToOutputBrand());
        }

        [HttpPut("BrandUpdate")]
        public ActionResult<OutputBrand> Update(int id, InputUpdateBrand input)
        {
            var result = _brandService.UpdateBrand(id, input);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            if (result is null)
            {
                return NotFound(result.Message);
            }

            var updatedBrand = result.Request;
            return Ok(updatedBrand.ToOutputBrand());
        }

        [HttpDelete("BrandDeletion")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _brandService.DeleteBrandAsync(id);
            return Ok(result);
        }
    }
}