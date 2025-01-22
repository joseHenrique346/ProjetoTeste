using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IProductValidateService
    {
        Task<BaseResponse<List<OutputProduct?>>> ValidateCreateProduct(List<InputCreateProduct> input);
        Task<BaseResponse<List<OutputProduct?>>> ValidateUpdateProduct(List<InputIdentityUpdateProduct> input);
        Task<BaseResponse<List<Product?>>> ValidateDeleteProduct(List<InputIdentityDeleteProduct> input);
    }
}