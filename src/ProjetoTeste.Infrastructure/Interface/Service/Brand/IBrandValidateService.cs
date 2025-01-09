using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandValidateService
    {
        Task<Response<InputCreateBrand?>> ValidateCreateBrand(InputCreateBrand input);
        Task<Response<InputUpdateBrand?>> ValidateUpdateBrand(long id, InputUpdateBrand input);
        Task<Response<string?>> ValidateDeleteBrand(long id);
    }
}