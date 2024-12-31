using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Conversor
{
    public static class ProductOrderMapExtension
    {
        public static OutputProductOrder ToOutputProductOrder(this ProductOrder productOrder)
        {
            return new OutputProductOrder(
                productOrder.Id,
                productOrder.OrderId,
                productOrder.ProductId,
                productOrder.Quantity,
                productOrder.UnitPrice
            );
        }

        public static ProductOrder ToProductOrder(this InputCreateProductOrder input)
        {
            return new ProductOrder
            {

            };
        }

        public static List<OutputProductOrder> ToListOutputProductOrder(this List<ProductOrder> productOrder)
        {
            return productOrder.Select(x => new OutputProductOrder(
                x.Id,
                x.OrderId,
                x.ProductId,
                x.Quantity,
                x.UnitPrice
            )).ToList();
        }
    }
}