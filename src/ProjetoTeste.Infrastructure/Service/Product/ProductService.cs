using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entities;
using System.Linq.Expressions;

namespace ProjetoTeste.Infrastructure.Service
{
    public class ProductService : IProductService
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

        public async Task<BaseResponse<List<OutputProduct>>> Create(List<InputCreateProduct> listInputCreateProduct)
        {
            var result = await _productValidateService.ValidateCreateProduct(listInputCreateProduct);
            if (!result.Success)
            {
                return new BaseResponse<List<OutputProduct>>
                {
                    Success = false,
                    Message = result.Message
                };
            }

            var product = await _productRepository.CreateAsync(listInputCreateProduct.ToListProduct());
            return new BaseResponse<List<OutputProduct>>
            {
                Success = true,
                Content = product.ToListOutputProduct()
            };
        }

        #endregion

        #region Update

        public async Task<BaseResponse<List<OutputProduct>>> Update(List<InputIdentityUpdateProduct> listInputIdentityUpdateProduct)
        {
            var response = new BaseResponse<OutputProduct>()
;
            var result = await _productValidateService.ValidateUpdateProduct(listInputIdentityUpdateProduct);
            if (!result.Success)
            {
                return new BaseResponse<List<OutputProduct>>
                {
                    Success = false,
                    Message = result.Message
                };
            }

            var currentProduct = await _productRepository.GetListByListIdWhere(listInputIdentityUpdateProduct.Select(i => i.Id).ToList());

            for (int i = 0; i < listInputIdentityUpdateProduct.Count; i++)
            {
                currentProduct[i].Name = listInputIdentityUpdateProduct[i].InputUpdateProduct.Name;
                currentProduct[i].Code = listInputIdentityUpdateProduct[i].InputUpdateProduct.Code;
                currentProduct[i].Description = listInputIdentityUpdateProduct[i].InputUpdateProduct.Description;
                currentProduct[i].Price = listInputIdentityUpdateProduct[i].InputUpdateProduct.Price;
                currentProduct[i].Stock = listInputIdentityUpdateProduct[i].InputUpdateProduct.Stock;
            }

            await _productRepository.Update(currentProduct);

            return new BaseResponse<List<OutputProduct>>
            {
                Success = true,
                Content = currentProduct.ToListOutputProduct()
            };
        }

        #endregion

        #region Delete

        public async Task<BaseResponse<List<string>>> Delete(List<InputIdentityDeleteProduct> listInputIdentityDeleteProduct)
        {
            var result = await _productValidateService.ValidateDeleteProduct(listInputIdentityDeleteProduct);
            if (!result.Success)
            {
                return new BaseResponse<List<string>>
                {
                    Success = false,
                    Message = result.Message
                };
            }

            await _productRepository.DeleteAsync(result.Content);
            return new BaseResponse<List<string>>
            {
                Success = true,
                Message = result.Message
            };
        }

        #endregion
    }
}