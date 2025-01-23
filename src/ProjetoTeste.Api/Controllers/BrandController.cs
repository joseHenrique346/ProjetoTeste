using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;

namespace ProjetoTeste.Api.Controllers
{
    public class BrandController : BaseController
    {
        #region Dependency Injection

        private readonly IBrandService _brandService;
        public BrandController(IUnitOfWork unitOfWork, IBrandService brandService) : base(unitOfWork)
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

        [HttpPost("GetById")]
        public async Task<ActionResult<List<OutputBrand>>> Get(List<InputIdentityViewBrand> listInputIdentityViewBrand)
        {
            var result = await _brandService.Get(listInputIdentityViewBrand);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var brand = result.Content;

            return Ok(brand.ToListOutputBrand());
        }

        #endregion

        #region Post

        [HttpPost]
        public async Task<ActionResult<BaseResponse<List<OutputBrand>>>> CreateAsync(List<InputCreateBrand> input)
        {
            var result = await _brandService.Create(input);

            if (result is null)
            {
                return NotFound(result);
            }

            if (!result.Success)
            {
                return BadRequest(result);
            }

            var newBrand = result;
            return Ok(newBrand);
        }

        #endregion

        #region Put

        [HttpPut]
        public async Task<ActionResult<BaseResponse<OutputBrand>>> Update(List<InputIdentityUpdateBrand> listInputIdentityUpdateBrand)
        {
            var result = await _brandService.Update(listInputIdentityUpdateBrand);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            if (result is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        #endregion

        #region Delete

        [HttpDelete]
        public async Task<ActionResult<List<string>>> Delete(List<InputIdentityDeleteBrand> listInputIdentityDeleteBrand)
        {
            var result = await _brandService.Delete(listInputIdentityDeleteBrand);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        #endregion
    }
}