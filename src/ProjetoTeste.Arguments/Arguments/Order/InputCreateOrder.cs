using ProjetoTeste.Arguments.Arguments.ProductOrder;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order
{
    [method: JsonConstructor]
    public class InputCreateOrder(long customerId, List<InputCreateProductOrder> productList)
    {
        public long CustomerId { get; } = customerId;
        public List<InputCreateProductOrder> ProductOrder { get; } = productList;

        [JsonIgnore]
        public DateTime CreatedDate { get; } = DateTime.Now.Date;
    }
}