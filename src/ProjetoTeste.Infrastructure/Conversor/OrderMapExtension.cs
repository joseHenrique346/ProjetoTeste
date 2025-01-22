﻿using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Conversor
{
    public static class OrderConverter
    {
        public static OutputOrder? ToOutputOrder(this Order? order)
        {
            if (order is null) return null;
            return order == null ? null : new OutputOrder
                (order.Id, 
                order.CustomerId, 
                order.ListProductOrder == null ? default : 
                (from i in order.ListProductOrder select i.ToOuputProductOrder()).ToList(), 
                order.Total, 
                order.CreatedDate);
        }

        public static List<OutputOrder>? ToListOutputOrder(this List<Order> orders)
        {
            return orders == null ? null : orders.Select(x => new OutputOrder(
                x.Id,
                x.CustomerId,
                x.ListProductOrder.ToListOuputProductOrder(),
                x.Total,
                x.CreatedDate
            )).ToList();
        }
    }
}