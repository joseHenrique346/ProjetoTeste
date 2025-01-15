using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<BaseResponse<Product>> Get(long id);
        Task<BaseResponse<Product>> Create(InputCreateProduct product);
        Task<BaseResponse<Product>> Update(InputUpdateProduct product);
        Task<BaseResponse<bool>> Delete(long id);
    }
}