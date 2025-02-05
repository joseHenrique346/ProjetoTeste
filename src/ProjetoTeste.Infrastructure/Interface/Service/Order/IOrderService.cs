using ProjetoTeste.Arguments.Arguments.Base.Inputs;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.Order.Reports.Outputs;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Base;

namespace ProjetoTeste.Infrastructure.Interface.Service
{
    public interface IOrderService : IBaseService<InputIdentityViewOrder, InputCreateOrder, BaseInputIdentityUpdate_0, BaseInputIdentityDelete_0, OutputOrder>
    {
        Task<BaseResponse<List<OutputProductOrder>>> CreateProductOrder(List<InputCreateProductOrder> listInputCreateProductOrder);
        Task<BaseResponse<OutputProductOrder>> CreateProductOrderSingle(InputCreateProductOrder inputCreateProductOrder);
        Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct();
        Task<OutputMaxSaleValueBrand> GetMostOrderedBrand();
        Task<OutputAverageSaleValueOrder> GetOrderAveragePrice();
        Task<List<OutputMinSaleValueProduct>> GetLeastOrderedProduct();
        Task<OutputMostOrderQuantityCustomer> GetMostOrdersCustomer();
        Task<OutputMostValueOrderCustomer> GetMostValueOrderClient();
    }
}