using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Conversor
{
    public static class ProductOrderMapExtension
    {
        public static ProductOrder? ToProductOrder(this InputCreateProductOrder? input)
        {
            return input == null ? default : new ProductOrder(input.OrderId, input.ProductId, input.Quantity);
        }

        public static OutputProductOrder? ToOuputProductOrder(this ProductOrder? productOrders)
        {
            return productOrders == null ? default : new OutputProductOrder(productOrders.Id, productOrders.OrderId, productOrders.ProductId, productOrders.Quantity, productOrders.UnitPrice, productOrders.SubTotal);
        }
    }
}