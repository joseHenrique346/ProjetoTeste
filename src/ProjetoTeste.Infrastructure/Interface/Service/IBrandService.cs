using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IBrandService
    {
        Task<List<Brand?>> GetAll();
        Task<Response<Brand?>> Get(long id);
        Task<Response<Brand?>> Create(InputCreateBrand product);
        Task<Response<Brand?>> Update(InputUpdateBrand product);
        Task<Response<bool>> Delete(long id);
    }
}
