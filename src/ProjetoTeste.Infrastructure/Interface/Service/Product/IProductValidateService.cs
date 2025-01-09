using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IProductValidateService
    {
        Task<Response<InputCreateProduct?>> ValidateCreateProduct(InputCreateProduct input);
        Task<Response<InputUpdateProduct?>> ValidateUpdateProduct(long id, InputUpdateProduct input);
        Task<Response<string?>> ValidateDeleteProduct(long id);
    }
}