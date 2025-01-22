using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAll();
        Task<BaseResponse<List<Brand>>> Get(List<InputIdentityViewBrand> input);
        Task<BaseResponse<List<OutputBrand>>> Create(List<InputCreateBrand> input);
        Task<BaseResponse<List<OutputBrand>>> Update(List<InputIdentityUpdateBrand> input);
        Task<BaseResponse<string>> Delete(List<InputIdentityDeleteBrand> input);
    }
}
