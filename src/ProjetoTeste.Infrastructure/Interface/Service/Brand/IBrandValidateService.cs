using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandValidateService
    {
        Task<BaseResponse<List<OutputBrand?>>> ValidateCreateBrand(List<InputCreateBrand> input);
        Task<BaseResponse<List<OutputBrand?>>> ValidateUpdateBrand(List<InputUpdateBrand> input);
        Task<BaseResponse<List<string?>>> ValidateDeleteBrand(List<long> id);
    }
}