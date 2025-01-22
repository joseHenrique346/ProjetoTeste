using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entities;

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

        public async Task<BaseResponse<List<OutputProduct?>>> ValidateCreateProduct(List<InputCreateProduct> listInputCreateProduct)
        {
            var response = new BaseResponse<List<OutputProduct?>>();

            var existingCodeProduct = await _productRepository.GetByCode(listInputCreateProduct.Select(i => i.Code).ToString());

            for (var i = 0; i < listInputCreateProduct.Count; i++)
            {
                if (existingCodeProduct is null)
                    response.AddErrorMessage("Já existe produto com este Código.");

                if (listInputCreateProduct[i].Name.Length > 40)
                    response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres");

                if (string.IsNullOrEmpty(listInputCreateProduct[i].Code))
                    response.AddErrorMessage("O código tem que ser preenchido!");

                if (listInputCreateProduct[i].Code.Length > 6)
                    response.AddErrorMessage("O código não pode ser maior que 6 caracteres.");

                if (string.IsNullOrEmpty(listInputCreateProduct[i].Description))
                    response.AddErrorMessage("O código não pode ser maior que 6 caracteres.");

                if (listInputCreateProduct[i].Description.Length > 100)
                    response.AddErrorMessage("A descrição não pode ser maior que 100 caracteres.");

                if (listInputCreateProduct[i].Stock <= 0)
                    response.AddErrorMessage("O estoque não pode ser menor ou igual a zero.");

                if (listInputCreateProduct[i].Stock.ToString().Length > 10)
                    response.AddErrorMessage("O estoque não pode ter este tamanho");

                if (listInputCreateProduct[i].Price < 0)
                    response.AddErrorMessage("O preço não pode ser menor que zero.");

                if (listInputCreateProduct[i].BrandId.HasValue && listInputCreateProduct[i].BrandId.Value <= 0)
                    response.AddErrorMessage("Informe um valor válido para o Id da marca.");

                if (!listInputCreateProduct[i].BrandId.HasValue)
                    response.AddErrorMessage("O ID da marca não pode ser nulo.");

                if (response.Message.Count > 0)
                {
                    response.Success = false;
                    listInputCreateProduct.Remove(listInputCreateProduct[i]);
                }
            }

            return response;
        }

        #endregion

        #region Validate Update

        public async Task<BaseResponse<List<OutputProduct?>>> ValidateUpdateProduct(List<InputIdentityUpdateProduct> listInputIdentityUpdateProduct)
        {
            var response = new BaseResponse<List<OutputProduct?>>();

            var currentProduct = await _productRepository.GetListByListIdWhere(listInputIdentityUpdateProduct.Select(i => i.Id).ToList());


            for (var i = 0; i < listInputIdentityUpdateProduct.Count; i++)
            {
                if (currentProduct[i] is null)
                    response.AddErrorMessage("Produto não encontrado.");

                var existingProduct = await _productRepository.GetByCode(listInputIdentityUpdateProduct.Select(i => i.InputUpdateProduct.Code).ToString());
                if (existingProduct != null && currentProduct[i].Id != listInputIdentityUpdateProduct[i].Id)
                    response.AddErrorMessage("Já existe produto com este Código.");

                if (listInputIdentityUpdateProduct[i].InputUpdateProduct.Name.ToString().Length > 40)
                    response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres");

                if (string.IsNullOrEmpty(listInputIdentityUpdateProduct[i].InputUpdateProduct.Code.ToString()))
                    response.AddErrorMessage("O código tem que ser preenchido!");

                if (listInputIdentityUpdateProduct[i].InputUpdateProduct.Code.ToString().Length > 6)
                    response.AddErrorMessage("O código não pode ser maior que 6 caracteres.");

                if (string.IsNullOrEmpty(listInputIdentityUpdateProduct[i].InputUpdateProduct.Description.ToString()))
                    response.AddErrorMessage("O código não pode ser maior que 6 caracteres.");

                if (listInputIdentityUpdateProduct[i].InputUpdateProduct.Description.ToString().Length > 100)
                    response.AddErrorMessage("A descrição não pode ser maior que 100 caracteres.");

                if (listInputIdentityUpdateProduct[i].InputUpdateProduct.Stock < 0)
                    response.AddErrorMessage("O estoque não pode ser menor que zero.");

                if (listInputIdentityUpdateProduct[i].InputUpdateProduct.Stock.ToString().Length > 10)
                    response.AddErrorMessage("O estoque não pode ter este tamanho");

                if (listInputIdentityUpdateProduct[i].InputUpdateProduct.Price < 0)
                    response.AddErrorMessage("O preço não pode ser menor que zero.");

                if (listInputIdentityUpdateProduct[i].InputUpdateProduct.BrandId.HasValue && listInputIdentityUpdateProduct[i].InputUpdateProduct.BrandId.Value <= 0)
                    response.AddErrorMessage("Informe um valor válido para o Id da marca.");

                if (!listInputIdentityUpdateProduct[i].InputUpdateProduct.BrandId.HasValue)
                    response.AddErrorMessage("O ID da marca não pode ser nulo.");
                
                if (response.Message.Count > 0)
                {
                    response.Success = false;
                    listInputIdentityUpdateProduct.Remove(listInputIdentityUpdateProduct[i]);
                }
            }

            return response;
        }

        #endregion

        #region Validate Delete

        public async Task<BaseResponse<List<Product?>>> ValidateDeleteProduct(List<InputIdentityDeleteProduct> listInputIdentityDeleteProduct)
        {
            var response = new BaseResponse<List<Product?>>();

            var products = await _productRepository.GetAllAsync();

            for (int i = 0; i < listInputIdentityDeleteProduct.Count; i++)
            {
                var existingProduct = products.FirstOrDefault(j => j.Id == listInputIdentityDeleteProduct[i].Id);

                if (existingProduct is null)
                    response.AddErrorMessage("Não foi encontrado o ID inserido, foi informado corretamente?");

                if (existingProduct.Stock > 0)
                    response.AddErrorMessage("Não foi possível deletar o produto, o mesmo ainda possue estoque");

                if (response.Message.Count > 0)
                {
                    response.Success = false;
                    listInputIdentityDeleteProduct.Remove(listInputIdentityDeleteProduct[i]);
                }
                else
                    response.AddSuccessMessage($"O produto {existingProduct.Name} foi deletado com sucesso");
            }

            response.Content = products;
            return response;
        }
    }
}
#endregion