using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.Order.Reports.Outputs;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IOrderService
    {
        Task<List<OutputOrder>> GetAll();
        Task<BaseResponse<List<OutputOrder>>> Get(List<InputIdentityViewOrder> listInputIdentityViewOrder);
        Task<BaseResponse<List<OutputOrder>>> Create(List<InputCreateOrder> listInputCreateOrder);
        Task<BaseResponse<List<OutputProductOrder>>> CreateProductOrder(List<InputCreateProductOrder> listInputCreateProductOrder);
        Task<OutputOrder> GetSingle(InputIdentityViewOrder inputIdentityViewOrder);
        Task<BaseResponse<OutputOrder>> CreateSingle(InputCreateOrder InputCreateOrder);
        Task<BaseResponse<OutputProductOrder>> CreateProductOrderSingle(InputCreateProductOrder inputCreateProductOrder);
        Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct();
        Task<OutputMaxSaleValueBrand> GetMostOrderedBrand();
        Task<OutputAverageSaleValueOrder> GetOrderAveragePrice();
        Task<List<OutputMinSaleValueProduct>> GetLeastOrderedProduct();
        Task<OutputMostOrderQuantityCustomer> GetMostOrdersCustomer();
        Task<OutputMostValueOrderCustomer> GetMostValueOrderClient();
    }
}