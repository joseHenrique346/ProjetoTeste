using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Api.Controllers
{
    public class BrandController : BaseController<IBrandService, Brand, InputIdentityViewBrand, InputCreateBrand, InputIdentityUpdateBrand, InputIdentityDeleteBrand, OutputBrand>
    {
        #region Dependency Injection

        private readonly IBrandService _brandService;
        private readonly IBrandService _service;
        public BrandController(IUnitOfWork unitOfWork, IBrandService brandService, IBrandService service) : base(unitOfWork, service)
        {
            _brandService = brandService;
            _service = service;
        }

        #endregion

        #region Get

        [HttpGet("GetAllWithProducts")]
        public async Task<ActionResult<List<OutputBrand>>> GetAllWithProducts()
        {
            return Ok(await _brandService.GetAllWithProducts());
        }

        #endregion
    }
}