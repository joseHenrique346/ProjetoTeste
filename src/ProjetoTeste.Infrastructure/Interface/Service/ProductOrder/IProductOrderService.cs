using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IProductOrderService
    {
        Task<BaseResponse<ProductOrder>> Create(InputCreateProductOrder input);
    }
}
