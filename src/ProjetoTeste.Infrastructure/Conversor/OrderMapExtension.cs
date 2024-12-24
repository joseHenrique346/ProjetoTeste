
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Conversor
{
    public static class OrderMapExtension
    {
        public static OutputOrder ToOutputOrder(this Order order)
        {
            return new OutputOrder(
                order.Id,
                order.ClientId,
                order.ProductOrders
            );
        }

        public static Order ToOrder(this InputCreateOrder input)
        {
            return new Order
            {
                ClientId = input.ClientId,
            };
        }

        public static List<OutputOrder> ToListOutputOrder(this List<Order> order)
        {
            return order.Select(x => new OutputOrder(
                x.Id,
                x.ClientId,
                x.ProductOrders
                )).ToList();
        }
    }
}
