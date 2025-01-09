using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;

namespace ProjetoTeste.Infrastructure.Service
{
    public class ProductValidateService : IProductValidateService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        public ProductValidateService(IProductRepository productRepository, IBrandRepository brandRepository)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
        }
        public async Task<Response<InputCreateProduct?>> ValidateCreateProduct(InputCreateProduct input)
        {
            var existingProduct = await _productRepository.GetByCode(input.Code);

            if (existingProduct != null)
            {
                return new Response<InputCreateProduct?>() { Success = false, Message = "Já existe produto com este Código." };
            }

            if (string.IsNullOrEmpty(input.Code))
            {
                return new Response<InputCreateProduct?>() { Success = false, Message = "O código tem que ser preenchido!" };
            }

            if (string.IsNullOrEmpty(input.Description))
            {
                return new Response<InputCreateProduct?>() { Success = false, Message = "A descrição tem que ser preenchida!" };
            }

            if (input.Stock < 0)
            {
                return new Response<InputCreateProduct?> { Success = false, Message = "O estoque não pode ser menor que zero." };
            }

            if (input.Price < 0)
            {
                return new Response<InputCreateProduct?> { Success = false, Message = "O preço não pode ser menor que zero." };
            }

            if (input.BrandId.HasValue && input.BrandId.Value <= 0)
            {
                return new Response<InputCreateProduct?> { Success = false, Message = "Informe um valor válido para o Id da marca." };
            }

            if (!input.BrandId.HasValue)
            {
                return new Response<InputCreateProduct?> { Success = false, Message = "O ID da marca não pode ser nulo." };
            }

            return new Response<InputCreateProduct?>() { Success = true, Request = input };
        }

        public async Task<Response<InputUpdateProduct?>> ValidateUpdateProduct(long id, InputUpdateProduct input)
        {
            var currentProduct = await _productRepository.GetAsync(id);

            if (currentProduct == null)
            {
                return new Response<InputUpdateProduct?>() { Success = false, Message = "Produto não encontrado." };
            }

            var existingCodeProduct = await _productRepository.GetByCode(input.Code);

            if (existingCodeProduct != null)
            {
                return new Response<InputUpdateProduct?>() { Success = false, Message = "Já existe um produto com este código." };
            }

            if (string.IsNullOrEmpty(input.Description))
            {
                return new Response<InputUpdateProduct?>() { Success = false, Message = "A descrição não pode ser vazia." };
            }

            return new Response<InputUpdateProduct?>() { Success = true, Request = input};
        }

        public async Task<Response<string?>> ValidateDeleteProduct(long id)
        {
            var products = await _productRepository.GetAllAsync();
            var existingProduct = products.FirstOrDefault(x => x.Id == id);

            if (existingProduct is null)
            {
                return new Response<string?>
                {
                    Success = false,
                    Message = "Não foi encontrado o ID inserido, foi informado corretamente?"
                };
            }

            return new Response<string?>
            {
                Success = true,
            };
        }
    }
}