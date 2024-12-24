using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTeste.Arguments.Arguments.ProductOrder
{
    public class InputCreateProductOrder(long orderId, long productId, int quantity, decimal unitPrice)
    {
        public long OrderId { get; } = orderId;
        public long ProductId { get; } = productId;
        public int Quantity { get; } = quantity;
        public decimal UnitPrice { get; } = unitPrice;
    }
}