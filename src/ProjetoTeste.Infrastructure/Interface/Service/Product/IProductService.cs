using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<BaseResponse<List<OutputProduct>>> Get(List<InputIdentityViewProduct> listInputIdentityViewProduct);
        Task<BaseResponse<List<OutputProduct>>> Create(List<InputCreateProduct> listInputCreateProduct);
        Task<BaseResponse<List<OutputProduct>>> Update(List<InputIdentityUpdateProduct> listInputIdentityUpdateProduct);
        Task<BaseResponse<bool>> Delete(List<InputIdentityDeleteProduct> listInputIdentityDeleteProduct);
        Task<OutputProduct> GetSingle(InputIdentityViewProduct inputIdentityViewProduct);
        Task<BaseResponse<OutputProduct>> CreateSingle(InputCreateProduct inputCreateProduct);
        Task<BaseResponse<OutputProduct>> UpdateSingle(InputIdentityUpdateProduct inputIdentityUpdateProduct);
        Task<BaseResponse<bool>> DeleteSingle(InputIdentityDeleteProduct inputIdentityDeleteProduct);
    }
}