using ProjetoTeste.Arguments.Arguments.ProductOrder;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace ProjetoTeste.Arguments.Arguments.Order
{
    public class OutputOrder
    {
        [JsonConstructor]
        public OutputOrder(long id, long clientId, List<InputCreateProductOrder> productList)
        {
            Id = id;
            ClientId = clientId;
            ProductList = productList ?? new List<InputCreateProductOrder>(); 
        }

        public long Id { get; set; }
        public long ClientId { get; }

        [JsonIgnore]
        public DateTime CreatedDate { get; } = DateTime.Now.Date;
        public List<InputCreateProductOrder> ProductList { get; }
    }
}