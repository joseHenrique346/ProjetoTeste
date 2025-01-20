using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entities;
using System.Linq.Expressions;

namespace ProjetoTeste.Infrastructure.Service
{
    public class ProductService
    {

        #region Dependency Injection

        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IProductValidateService _productValidateService;
        public ProductService(IProductRepository productRepository, IBrandRepository brandRepository, IProductValidateService productValidateService)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _productValidateService = productValidateService;
        }

        #endregion

        #region Get

        public async Task<Product?> Get(long id)
        {
            return await _productRepository.GetWithIncludesAsync(id, p => p.Brand);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetWithIncludesAsync(long id, params Expression<Func<Product, object>>[] includes)
        {
            return await _productRepository.GetWithIncludesAsync(id, includes);
        }

        #endregion

        #region Create

        public async Task<BaseResponse<OutputProduct>> Create(InputCreateProduct input)
        {
            var result = await _productValidateService.ValidateCreateProduct(input);
            if (!result.Success)
            {
                return new BaseResponse<OutputProduct>
                {
                    Success = false,
                    Message = result.Message
                };
            }

            var product = await _productRepository.CreateAsync(input.ToProduct());
            return new BaseResponse<OutputProduct>
            {
                Success = true,
                Content = product.ToOutputProduct()
            };
        }

        #endregion

        #region Update

        public async Task<BaseResponse<Product>> Update(InputUpdateProduct input)
        {
            var response = new BaseResponse<Product>()
;
            var result = await _productValidateService.ValidateUpdateProduct(input);
            if (!result.Success)
            {
                return new BaseResponse<Product>
                {
                    Success = false,
                    Message = result.Message
                };
            }

            var existingProduct = await _productRepository.GetListByListId(input.Id);

            existingProduct.Name = input.Name;
            existingProduct.Code = input.Code;
            existingProduct.Description = input.Description;
            existingProduct.Price = input.Price;
            existingProduct.Stock = input.Stock;
            existingProduct.BrandId = input.BrandId;

            await _productRepository.Update(existingProduct);

            return new BaseResponse<Product>
            {
                Success = true,
                Content = existingProduct
            };
        }

        #endregion

        #region Delete

        public async Task<BaseResponse<string>> Delete(long id)
        {
            var result = await _productValidateService.ValidateDeleteProduct(id);
            if (!result.Success)
            {
                return new BaseResponse<string>
                {
                    Success = false,
                    Message = result.Message
                };
            }

            await _productRepository.DeleteAsync(id);
            return new BaseResponse<string>
            {
                Success = true,
                Message = result.Message
            };
        }

        #endregion
    }
}