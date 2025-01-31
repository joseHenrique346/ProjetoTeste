﻿using Microsoft.AspNetCore.Mvc;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;
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

        [HttpPost("GetBySingleId")]
        public async Task<ActionResult<OutputBrand>> GetSingle(InputIdentityViewBrand inputIdentityViewBrand)
        {
            return Ok(await _brandService.GetSingle(inputIdentityViewBrand));
        }

        [HttpGet]
        public async Task<ActionResult<List<OutputBrand>>> GetAll()
        {
            return Ok(await _brandService.GetAll());
        }

        [HttpPost("GetByListId")]
        public async Task<ActionResult<List<OutputBrand>>> Get(List<InputIdentityViewBrand> listInputIdentityViewBrand)
        {
            return Ok(await _brandService.Get(listInputIdentityViewBrand));
        }

        #endregion

        #region Post

        [HttpPost("Single")]
        public async Task<ActionResult<BaseResponse<OutputBrand>>> CreateSingle(InputCreateBrand inputCreateBrand)
        {
            var result = await _brandService.CreateSingle(inputCreateBrand);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            var newBrand = result;
            return Ok(newBrand);
        }

        [HttpPost("Multiple")]
        public async Task<ActionResult<BaseResponse<List<OutputBrand>>>> CreateAsync(List<InputCreateBrand> listInputCreateBrand)
        {
            var result = await _brandService.Create(listInputCreateBrand);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            var newBrand = result;
            return Ok(newBrand);
        }

        #endregion

        #region Put

        [HttpPut("Single")]
        public async Task<ActionResult<BaseResponse<OutputBrand>>> Update(InputIdentityUpdateBrand inputIdentityUpdateBrand)
        {
            var result = await _brandService.UpdateSingle(inputIdentityUpdateBrand);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("Multiple")]
        public async Task<ActionResult<BaseResponse<OutputBrand>>> Update(List<InputIdentityUpdateBrand> listInputIdentityUpdateBrand)
        {
            var result = await _brandService.Update(listInputIdentityUpdateBrand);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        #endregion

        #region Delete

        [HttpDelete("Single")]
        public async Task<ActionResult<string>> DeleteSingle(InputIdentityDeleteBrand inputIdentityDeleteBrand)
        {
            var result = await _brandService.DeleteSingle(inputIdentityDeleteBrand);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("Multiple")]
        public async Task<ActionResult<List<string>>> Delete(List<InputIdentityDeleteBrand> listInputIdentityDeleteBrand)
        {
            var result = await _brandService.Delete(listInputIdentityDeleteBrand);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}

#endregion