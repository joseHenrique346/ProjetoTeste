using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IProductValidateService
    {
        Task<BaseResponse<List<ProductValidate?>>> ValidateCreateProduct(List<ProductValidate> input);
        Task<BaseResponse<List<ProductValidate?>>> ValidateUpdateProduct(List<ProductValidate> input);
        Task<BaseResponse<List<ProductValidate?>>> ValidateDeleteProduct(List<ProductValidate> input);
    }
}