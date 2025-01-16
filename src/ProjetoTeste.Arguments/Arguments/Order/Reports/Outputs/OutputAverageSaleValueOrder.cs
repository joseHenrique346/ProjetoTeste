using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order.Reports.Outputs
{
    [method: JsonConstructor]
    public class OutputAverageSaleValueOrder(long orderId, int quantity, decimal total, decimal average)
    {
        public long OrderId { get; } = orderId;
        public int Quantity { get; } = quantity;
        public decimal Total { get; } = total;
        public decimal AveragePrice { get; } = average;
    }
}
