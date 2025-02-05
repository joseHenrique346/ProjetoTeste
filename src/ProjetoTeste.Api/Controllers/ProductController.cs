using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Api.Controllers
{
    public class ProductController : BaseController<IProductService, Product, InputIdentityViewProduct, InputCreateProduct, InputIdentityUpdateProduct, InputIdentityDeleteProduct, OutputProduct>
    {
        #region Dependency Injection

        private readonly IProductService _service;
        public ProductController(IUnitOfWork unitOfWork, IProductService service) : base(unitOfWork, service)
        {
            _service = service;
        }

        #endregion
    }
}