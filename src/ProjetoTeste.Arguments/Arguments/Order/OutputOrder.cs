using ProjetoTeste.Arguments.Arguments.Base.Outputs;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order;

public class OutputOrder : BaseOutput<OutputOrder>
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public List<OutputProductOrder> ProductOrders { get; set; }
    public decimal Total { get; set; }
    public DateTime OrderDate { get; set; }
    public OutputOrder() { }

    [JsonConstructor]
    public OutputOrder(long id, long customerId, List<OutputProductOrder> productOrders, decimal total, DateTime orderDate)
    {
        Id = id;
        CustomerId = customerId;
        ProductOrders = productOrders;
        Total = total;
        OrderDate = orderDate;
    }
}