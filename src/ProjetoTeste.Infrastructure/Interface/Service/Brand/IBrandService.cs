using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAll();
        Task<BaseResponse<List<OutputBrand>>> Get(List<InputIdentityViewBrand> listInputIdentityViewBrand);
        Task<BaseResponse<List<OutputBrand>>> Create(List<InputCreateBrand> listInputCreateBrand);
        Task<BaseResponse<List<OutputBrand>>> Update(List<InputIdentityUpdateBrand> inputIdentityUpdateBrand);
        Task<BaseResponse<bool>> Delete(List<InputIdentityDeleteBrand> listInputIdentityDeleteBrand);
        Task<BaseResponse<OutputBrand>> CreateSingle(InputCreateBrand inputCreateBrand);
        Task<BaseResponse<OutputBrand>> UpdateSingle(InputIdentityUpdateBrand inputIdentityUpdateBrand);
        Task<BaseResponse<bool>> DeleteSingle(InputIdentityDeleteBrand inputIdentityDeleteBrand);
    }
}
