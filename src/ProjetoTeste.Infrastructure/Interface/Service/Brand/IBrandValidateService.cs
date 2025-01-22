using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandValidateService
    {
        Task<BaseResponse<List<OutputBrand?>>> ValidateCreateBrand(List<InputCreateBrand> input);
        Task<BaseResponse<List<InputIdentityUpdateBrand?>>> ValidateUpdateBrand(List<InputIdentityUpdateBrand> input);
        Task<BaseResponse<List<string?>>> ValidateDeleteBrand(List<InputIdentityDeleteBrand> input);
    }
}