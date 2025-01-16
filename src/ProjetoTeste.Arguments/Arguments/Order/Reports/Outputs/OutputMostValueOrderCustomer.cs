using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order.Reports.Outputs;

[method: JsonConstructor]
public class OutputMostValueOrderCustomer(long customerId, string name, decimal totalValue, long quantity)
{
    public long CustomerId { get; private set; } = customerId;
    public string Name { get; private set; } = name;
    public decimal TotalValue { get; private set; } = totalValue;
    public long Quantity { get; private set; } = quantity;
}