using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.ProductOrder
{
    [method: JsonConstructor]
    public class OutputProductOrder(long id, long orderId, long productId, int quantity, decimal unitPrice, decimal totalPrice)
    {
        public long Id { get; set; } = id;
        public long OrderId { get; } = orderId;
        public long ProductId { get; } = productId;
        public int Quantity { get; } = quantity;
        public decimal UnitPrice { get; } = unitPrice;
        public decimal TotalPrice { get; } = totalPrice;
    }
}
