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

        public static List<OutputProductOrder?> ToListOuputProductOrder(this List<ProductOrder?> productOrders)
        {
            var newListProductOrder = from i in productOrders
                                      let newList = productOrders == null ? default : new OutputProductOrder(i.Id, i.OrderId, i.ProductId, i.Quantity, i.UnitPrice, i.SubTotal)
                                      select newList;

            return newListProductOrder.ToList();
        }

    }
}