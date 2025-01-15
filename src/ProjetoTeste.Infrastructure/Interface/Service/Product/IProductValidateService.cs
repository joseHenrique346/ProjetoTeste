using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IProductValidateService
    {
        Task<BaseResponse<InputCreateProduct?>> ValidateCreateProduct(InputCreateProduct input);
        Task<BaseResponse<InputUpdateProduct?>> ValidateUpdateProduct(InputUpdateProduct input);
        Task<BaseResponse<string?>> ValidateDeleteProduct(long id);
    }
}