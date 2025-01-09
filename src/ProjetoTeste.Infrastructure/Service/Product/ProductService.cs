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
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IProductValidateService _productValidateService;
        public ProductService(IProductRepository productRepository, IBrandRepository brandRepository, IProductValidateService productValidateService)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _productValidateService = productValidateService;
        }

        public async Task<Response<Product?>> Get(long id)
        {
            var product = await _productRepository.GetWithIncludesAsync(id, p => p.Brand);
            return new Response<Product?>
            {
                Success = true,
                Request = product
            };
        }

        public async Task<List<Product>> GetAll()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetWithIncludesAsync(long id, params Expression<Func<Product, object>>[] includes)
        {
            return await _productRepository.GetWithIncludesAsync(id, includes);
        }


        public async Task<Response<Product>> Create(InputCreateProduct input)
        {
            var result = await _productValidateService.ValidateCreateProduct(input);
            if (!result.Success)
            {
                return new Response<Product>
                {
                    Success = false,
                    Message = result.Message
                };
            }

            var product = await _productRepository.CreateAsync(input.ToProduct());
            return new Response<Product>
            {
                Success = true,
                Request = product
            };
        }

        public async Task<Response<Product>> Update(long id, InputUpdateProduct input)
        {
            var result = await _productValidateService.ValidateUpdateProduct(id, input);
            if (!result.Success)
            {
                return new Response<Product>
                {
                    Success = false,
                    Message = result.Message
                };
            }

            var existingProduct = await _productRepository.GetAsync(id);

            if (existingProduct == null)
            {
                return new Response<Product>
                {
                    Success = false,
                    Message = "Produto não encontrado."
                };
            }

            existingProduct.Name = input.Name;
            existingProduct.Code = input.Code;
            existingProduct.Description = input.Description;
            existingProduct.Price = input.Price;
            existingProduct.Stock = input.Stock;
            existingProduct.BrandId = input.BrandId;

            await _productRepository.Update(existingProduct);

            return new Response<Product>
            {
                Success = true,
                Request = existingProduct
            };
        }

        public async Task<Response<bool>> Delete(long id)
        {
            var result = await _productValidateService.ValidateDeleteProduct(id);
            if (!result.Success)
            {
                return new Response<bool>
                {
                    Success = false,
                    Message = result.Message
                };
            }

            await _productRepository.DeleteAsync(id);
            return new Response<bool>
            {
                Success = true
            };
        }
    }
}