using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAll();
        Task<BaseResponse<Brand>> Get(long id);
        Task<BaseResponse<Brand>> Create(InputCreateBrand product);
        Task<BaseResponse<Brand>> Update(InputUpdateBrand product);
        Task<BaseResponse<bool>> Delete(long id);
    }
}
