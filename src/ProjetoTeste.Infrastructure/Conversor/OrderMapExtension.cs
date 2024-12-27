using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Conversor
{
    public static class OrderMapExtension
    {
        public static OutputOrder ToOutputOrder(this Order order)
        {
            //var productOrderDto = order.ProductOrders.Select(po => new ProductList
            //{
            //    OrderId = po.OrderId,
            //    ProductId = po.ProductId,
            //    Quantity = po.Quantity,
            //}).ToList();

            return new OutputOrder(
                order.Id,
                order.CustomerId,
                default
                //productOrderDto
            );
        }

        public static Order ToOrder(this InputCreateOrder input)
        {
            var productOrders = input.ProductOrder.Select(dto => new ProductOrder
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
            }).ToList();

            return new Order
            {
                CustomerId = input.CustomerId,
                ProductOrders = productOrders
            };
        }

        public static List<OutputOrder> ToListOutputOrder(this List<Order> orders)
        {
            return orders.Select(order => new OutputOrder(
                order.Id,
                order.CustomerId,
                default
                //order.ProductOrders.Select(po => new ProductList
                //{
                //    OrderId = po.OrderId,
                //    ProductId = po.ProductId,
                //    Quantity = po.Quantity,
                //}).ToList()
            )).ToList();
        }
    }
}