using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Infrastructure.Interface.Base;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandService : IBaseService<InputIdentityViewBrand, InputCreateBrand, InputIdentityUpdateBrand, InputIdentityDeleteBrand, OutputBrand>
    {
        Task<List<OutputBrandWithProducts>> GetAllWithProducts();
        //Task<List<OutputBrand>> Get(List<InputIdentityViewBrand> listInputIdentityViewBrand);
        //Task<BaseResponse<List<OutputBrand>>> Create(List<InputCreateBrand> listInputCreateBrand);
        //Task<BaseResponse<List<OutputBrand>>> Update(List<InputIdentityUpdateBrand> inputIdentityUpdateBrand);
        //Task<BaseResponse<bool>> Delete(List<InputIdentityDeleteBrand> listInputIdentityDeleteBrand);
        //Task<OutputBrand> GetSingle(InputIdentityViewBrand inputIdentityViewBrand);
        //Task<BaseResponse<OutputBrand>> CreateSingle(InputCreateBrand inputCreateBrand);
        //Task<BaseResponse<OutputBrand>> UpdateSingle(InputIdentityUpdateBrand inputIdentityUpdateBrand);
        //Task<BaseResponse<bool>> DeleteSingle(InputIdentityDeleteBrand inputIdentityDeleteBrand);
    }
}