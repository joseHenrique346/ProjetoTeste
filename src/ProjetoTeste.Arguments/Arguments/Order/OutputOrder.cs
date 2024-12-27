using System.Text.Json.Serialization;
using ProjetoTeste.Arguments.Arguments.ProductOrder;

namespace ProjetoTeste.Arguments.Arguments.Order
{
    public class OutputOrder
    {
        [JsonConstructor]
        public OutputOrder(long id, long customerId, List<InputCreateProductOrder> productList)
        {
            Id = id;
            CustomerId = customerId;
            ProductList = productList ?? new List<InputCreateProductOrder>(); 
        }

        public long Id { get; set; }
        public long CustomerId { get; }

        [JsonIgnore]
        public DateTime CreatedDate { get; } = DateTime.Now.Date;
        public List<InputCreateProductOrder> ProductList { get; }
    }
}