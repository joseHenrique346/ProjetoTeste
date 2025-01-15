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
            var response = new BaseResponse<InputCreateProduct?>();

            var existingProduct = await _productRepository.GetByCode(input.Code);

            if (existingProduct != null)
                response.AddErrorMessage("Já existe produto com este Código.");

            if (input.Name.Length > 40)
                response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres");

            if (string.IsNullOrEmpty(input.Code))
                response.AddErrorMessage("O código tem que ser preenchido!");

            if (input.Code.Length > 6)
                response.AddErrorMessage("O código não pode ser maior que 6 caracteres.");

            if (string.IsNullOrEmpty(input.Description))
                response.AddErrorMessage("O código não pode ser maior que 6 caracteres.");

            if (input.Description.Length > 100)
                response.AddErrorMessage("A descrição não pode ser maior que 100 caracteres.");

            if (input.Stock <= 0)
                response.AddErrorMessage("O estoque não pode ser menor ou igual a zero.");

            if (input.Stock.ToString().Length > 10)
                response.AddErrorMessage("O estoque não pode ter este tamanho");

            if (input.Price < 0)
                response.AddErrorMessage("O preço não pode ser menor que zero.");

            if (input.BrandId.HasValue && input.BrandId.Value <= 0)
                response.AddErrorMessage("Informe um valor válido para o Id da marca.");

            if (!input.BrandId.HasValue)
                response.AddErrorMessage("O ID da marca não pode ser nulo.");

            if (response.Message.Count > 0)
                response.Success = false;

            return response;
        }

        #endregion

        #region Validate Update

        public async Task<BaseResponse<InputUpdateProduct?>> ValidateUpdateProduct(InputUpdateProduct input)
        {
            var response = new BaseResponse<InputUpdateProduct?>();

            var currentProduct = await _productRepository.GetAsync(input.Id);

            if (currentProduct == null)
                response.AddErrorMessage("Produto não encontrado.");

            var existingCodeProduct = await _productRepository.GetByCode(input.Code);

            var existingProduct = await _productRepository.GetByCode(input.Code);
            if (existingProduct != null)
                response.AddErrorMessage("Já existe produto com este Código.");

            if (input.Name.Length > 40)
                response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres");

            if (string.IsNullOrEmpty(input.Code))
                response.AddErrorMessage("O código tem que ser preenchido!");

            if (input.Code.Length > 6)
                response.AddErrorMessage("O código não pode ser maior que 6 caracteres.");

            if (string.IsNullOrEmpty(input.Description))
                response.AddErrorMessage("O código não pode ser maior que 6 caracteres.");

            if (input.Description.Length > 100)
                response.AddErrorMessage("A descrição não pode ser maior que 100 caracteres.");

            if (input.Stock <= 0)
                response.AddErrorMessage("O estoque não pode ser menor ou igual a zero.");

            if (input.Stock.ToString().Length > 10)
                response.AddErrorMessage("O estoque não pode ter este tamanho");

            if (input.Price < 0)
                response.AddErrorMessage("O preço não pode ser menor que zero.");

            if (input.BrandId.HasValue && input.BrandId.Value <= 0)
                response.AddErrorMessage("Informe um valor válido para o Id da marca.");

            if (!input.BrandId.HasValue)
                response.AddErrorMessage("O ID da marca não pode ser nulo.");

            if (response.Message.Count > 0)
                response.Success = false;

            return response;
        }

        #endregion

        #region Validate Delete

        public async Task<BaseResponse<string?>> ValidateDeleteProduct(long id)
        {
            var response = new BaseResponse<string?>();

            var products = await _productRepository.GetAllAsync();
            var existingProduct = products.FirstOrDefault(x => x.Id == id);

            if (existingProduct is null)
                response.AddErrorMessage("Não foi encontrado o ID inserido, foi informado corretamente?");

            if (response.Message.Count > 0)
                response.Success = false;
            else
                response.AddSuccessMessage($"O produto {existingProduct.Name} foi deletado com sucesso");

            return response;
        }

        #endregion
    }
}