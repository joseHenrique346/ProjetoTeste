using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Interface.Base;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IProductService : IBaseService<InputIdentityViewProduct, InputCreateProduct, InputIdentityUpdateProduct, InputIdentityDeleteProduct, OutputProduct> { }
}