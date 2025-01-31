using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandService
    {
        Task<List<OutputBrandWithProducts>> GetAll();
        Task<List<OutputBrand>> Get(List<InputIdentityViewBrand> listInputIdentityViewBrand);
        Task<BaseResponse<List<OutputBrand>>> Create(List<InputCreateBrand> listInputCreateBrand);
        Task<BaseResponse<List<OutputBrand>>> Update(List<InputIdentityUpdateBrand> inputIdentityUpdateBrand);
        Task<BaseResponse<bool>> Delete(List<InputIdentityDeleteBrand> listInputIdentityDeleteBrand);
        Task<OutputBrand> GetSingle(InputIdentityViewBrand inputIdentityViewBrand);
        Task<BaseResponse<OutputBrand>> CreateSingle(InputCreateBrand inputCreateBrand);
        Task<BaseResponse<OutputBrand>> UpdateSingle(InputIdentityUpdateBrand inputIdentityUpdateBrand);
        Task<BaseResponse<bool>> DeleteSingle(InputIdentityDeleteBrand inputIdentityDeleteBrand);
    }
}