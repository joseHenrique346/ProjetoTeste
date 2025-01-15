using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandValidateService
    {
        Task<BaseResponse<InputCreateBrand?>> ValidateCreateBrand(InputCreateBrand input);
        Task<BaseResponse<InputUpdateBrand?>> ValidateUpdateBrand(InputUpdateBrand input);
        Task<BaseResponse<string?>> ValidateDeleteBrand(long id);
    }
}