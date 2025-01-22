﻿//using Microsoft.AspNetCore.Mvc;
//using ProjetoTeste.Arguments.Arguments.Product;
//using ProjetoTeste.Infrastructure.Conversor;
//using ProjetoTeste.Infrastructure.Interface.Service;
//using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
//using ProjetoTeste.Infrastructure.Persistence.Entities;

//namespace ProjetoTeste.Api.Controllers
//{
//    public class ProductController : BaseController
//    {
//        #region Dependency Injection

//        private readonly IProductService _productService;
//        public ProductController(IUnitOfWork unitOfWork, IProductService productService) : base(unitOfWork)
//        {
//            _productService = productService;
//        }

//        #endregion

//        #region Get

//        [HttpGet]
//        public async Task<ActionResult<List<OutputProduct>>> GetAll()
//        {
//            var result = await _productService.GetAll();
//            return Ok(result.ToListOutputProduct());
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Product>> GetId(long id)
//        {
//            var result = await _productService.Get(id);
//            return Ok(result.ToOutputProduct());
//        }

//        #endregion

//        #region Post

//        [HttpPost]
//        public async Task<ActionResult<OutputProduct>> Create(InputCreateProduct input)
//        {
//            var result = await _productService.Create(input);

//            if (!result.Success)
//            {
//                return BadRequest(result.Message);
//            }

//            if (result is null)
//            {
//                return NotFound(result.Message);
//            }

//            var createdProduct = result.Content;
//            return Ok(createdProduct);
//        }

//        #endregion

//        #region Put

//        [HttpPut]
//        public async Task<ActionResult<OutputProduct>> Update(InputUpdateProduct input)
//        {
//            var result = await _productService.Update(input);

//            if (!result.Success)
//            {
//                return BadRequest(result.Message);
//            }

//            if (result is null)
//            {
//                return NotFound(result.Message);
//            }

//            var updatedProduct = result.Content;
//            return Ok(updatedProduct.ToOutputProduct());
//        }

//        #endregion

//        #region Delete

//        [HttpDelete]
//        public async Task<ActionResult<string>> Delete(int id)
//        {
//            var result = await _productService.Delete(id);

//            if (!result.Success)
//            {
//                return BadRequest(result.Message);
//            }

//            return Ok(result.Message);
//        }

//        #endregion
//    }
//}