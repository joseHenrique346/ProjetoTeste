using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;

namespace ProjetoTeste.Infrastructure.Service
{
    public class ProductValidateService : IProductValidateService
    {
        #region Dependency Injection
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        public ProductValidateService(IProductRepository productRepository, IBrandRepository brandRepository)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
        }

        #endregion

        #region Validate Create

        public async Task<BaseResponse<InputCreateProduct?>> ValidateCreateProduct(InputCreateProduct input)
        {
            var existingProduct = await _productRepository.GetByCode(input.Code);

            if (existingProduct != null)
            {
                return new BaseResponse<InputCreateProduct?>() { Success = false, Message = "Já existe produto com este Código." };
            }

            if (input.Name.Length > 40)
            {
                return new BaseResponse<InputCreateProduct?>() { Success = false, Message = "O nome não pode ultrapassar 40 caracteres" };
            }

            if (string.IsNullOrEmpty(input.Code))
            {
                return new BaseResponse<InputCreateProduct?>() { Success = false, Message = "O código tem que ser preenchido!" };
            }

            if (input.Code.Length > 6)
            {
                return new BaseResponse<InputCreateProduct?> { Success = false, Message = "O código não pode ser maior que 6 caracteres." };
            }

            if (string.IsNullOrEmpty(input.Description))
            {
                return new BaseResponse<InputCreateProduct?>() { Success = false, Message = "A descrição tem que ser preenchida!" };
            }

            if (input.Description.Length > 100)
            {
                return new BaseResponse<InputCreateProduct?> { Success = false, Message = "A descrição não pode ser maior que 100 caracteres." };
            }

            if (input.Stock < 0)
            {
                return new BaseResponse<InputCreateProduct?> { Success = false, Message = "O estoque não pode ser menor que zero." };
            }

            if (input.Stock.ToString().Length > 10)
            {
                return new BaseResponse<InputCreateProduct?> { Success = false, Message = "O estoque não pode ter este tamanho" };
            }

            if (input.Price < 0)
            {
                return new BaseResponse<InputCreateProduct?> { Success = false, Message = "O preço não pode ser menor que zero." };
            }

            if (input.BrandId.HasValue && input.BrandId.Value <= 0)
            {
                return new BaseResponse<InputCreateProduct?> { Success = false, Message = "Informe um valor válido para o Id da marca." };
            }

            if (!input.BrandId.HasValue)
            {
                return new BaseResponse<InputCreateProduct?> { Success = false, Message = "O ID da marca não pode ser nulo." };
            }

            return new BaseResponse<InputCreateProduct?>() { Success = true, Request = input };
        }

        #endregion

        #region Validate Update

        public async Task<BaseResponse<InputUpdateProduct?>> ValidateUpdateProduct(InputUpdateProduct input)
        {
            var currentProduct = await _productRepository.GetAsync(input.Id);

            if (currentProduct == null)
            {
                return new BaseResponse<InputUpdateProduct?>() { Success = false, Message = "Produto não encontrado." };
            }

            var existingCodeProduct = await _productRepository.GetByCode(input.Code);

            if (input.Name.Length > 40)
            {
                return new BaseResponse<InputUpdateProduct?>() { Success = false, Message = "O nome não pode ultrapassar 40 caracteres" };
            }

            if (existingCodeProduct != null && currentProduct.Id != input.Id)
            {
                return new BaseResponse<InputUpdateProduct?>() { Success = false, Message = "Já existe um produto com este código." };
            }

            if (string.IsNullOrEmpty(input.Code))
            {
                return new BaseResponse<InputUpdateProduct?>() { Success = false, Message = "O código tem que ser preenchido!" };
            }

            if (input.Code.Length > 6)
            {
                return new BaseResponse<InputUpdateProduct?> { Success = false, Message = "O código não pode ser maior que 6 caracteres." };
            }

            if (string.IsNullOrEmpty(input.Description))
            {
                return new BaseResponse<InputUpdateProduct?>() { Success = false, Message = "A descrição tem que ser preenchida!" };
            }

            if (input.Description.Length > 100)
            {
                return new BaseResponse<InputUpdateProduct?> { Success = false, Message = "A descrição não pode ser maior que 100 caracteres." };
            }

            if (input.Stock < 0)
            {
                return new BaseResponse<InputUpdateProduct?> { Success = false, Message = "O estoque não pode ser menor que zero." };
            }

            if (input.Stock.ToString().Length > 10)
            {
                return new BaseResponse<InputUpdateProduct?> { Success = false, Message = "O estoque não pode ter este tamanho" };
            }

            if (input.Price < 0)
            {
                return new BaseResponse<InputUpdateProduct?> { Success = false, Message = "O preço não pode ser menor que zero." };
            }

            if (input.BrandId.HasValue && input.BrandId.Value <= 0)
            {
                return new BaseResponse<InputUpdateProduct?> { Success = false, Message = "Informe um valor válido para o Id da marca." };
            }

            if (!input.BrandId.HasValue)
            {
                return new BaseResponse<InputUpdateProduct?> { Success = false, Message = "O ID da marca não pode ser nulo." };
            }

            return new BaseResponse<InputUpdateProduct?>() { Success = true, Request = input};
        }

        #endregion

        #region Validate Delete

        public async Task<BaseResponse<string?>> ValidateDeleteProduct(long id)
        {
            var products = await _productRepository.GetAllAsync();
            var existingProduct = products.FirstOrDefault(x => x.Id == id);

            if (existingProduct is null)
            {
                return new BaseResponse<string?>
                {
                    Success = false,
                    Message = "Não foi encontrado o ID inserido, foi informado corretamente?"
                };
            }

            return new BaseResponse<string?>
            {
                Success = true,
                Message = $"O produto {existingProduct.Name} foi deletado com sucesso"
            };
        }

        #endregion
    }
}