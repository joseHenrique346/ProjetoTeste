using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<Response<Product>> Get(long id);
        Task<Response<Product>> Create(InputCreateProduct product);
        Task<Response<Product>> Update(InputUpdateProduct product);
        Task<Response<bool>> Delete(long id);
    }
}
