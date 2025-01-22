using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<Product> Get(long id);
        Task<BaseResponse<List<OutputProduct>>> Create(List<InputCreateProduct> product);
        Task<BaseResponse<List<OutputProduct>>> Update(List<InputIdentityUpdateProduct> product);
        Task<BaseResponse<List<string>>> Delete(List<InputIdentityDeleteProduct> input);
    }
}