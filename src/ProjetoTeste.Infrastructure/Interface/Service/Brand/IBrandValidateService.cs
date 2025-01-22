using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandValidateService
    {
        Task<BaseResponse<List<BrandValidate?>>> ValidateCreateBrand(List<BrandValidate> input);
        Task<BaseResponse<List<BrandValidate?>>> ValidateUpdateBrand(List<BrandValidate> input);
        Task<BaseResponse<List<string?>>> ValidateDeleteBrand(List<InputIdentityDeleteBrand> input);
    }
}