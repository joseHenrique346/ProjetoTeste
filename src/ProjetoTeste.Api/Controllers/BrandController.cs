using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Service;

namespace ProjetoTeste.Api.Controllers
{
    public class BrandController : BaseController
    {
        #region Dependency Injection

        private readonly BrandService _brandService;

        public BrandController(IUnitOfWork unitOfWork, BrandService brandService) : base(unitOfWork)
        {
            _brandService = brandService;
        }

        #endregion

        #region Get

        [HttpGet]
        public async Task<ActionResult<List<OutputBrand>>> GetAll()
        {
            var brands = await _brandService.GetAll();
            return Ok(brands.ToListOutputBrand());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OutputBrand>> Get(int id)
        {
            var result = await _brandService.Get(id);
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

        #endregion

        #region Post

        [HttpPost]
        public async Task<ActionResult<OutputBrand>> CreateAsync(InputCreateBrand input)
        {
            var brand = await _brandService.Create(input);

            if (brand is null)
            {
                return NotFound(brand.Message);
            }

            if (!brand.Success)
            {
                return BadRequest(brand.Message);
            }

            var newBrand = brand.Request;
            return Ok(newBrand.ToOutputBrand());
        }

        #endregion

        #region Put

        [HttpPut]
        public async Task<ActionResult<OutputBrand>> Update(InputUpdateBrand input)
        {
            var result = await _brandService.Update(input);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            if (result is null)
            {
                return NotFound(result.Message);
            }

            var updatedBrand = result.Request;
            return Ok(updatedBrand);
        }

        #endregion

        #region Delete

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await _brandService.Delete(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            
            return Ok(result.Message);
        }

        #endregion
    }
}