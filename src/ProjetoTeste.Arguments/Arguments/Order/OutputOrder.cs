using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order
{
    [method: JsonConstructor]
    public class OutputOrder(long id, long clientId, List<ProductOrder> productList)
    {
        public long Id { get; set; } = id;
        public long ClientId { get; } = clientId;
        [JsonIgnore]
        public DateTime CreatedDate { get; } = DateTime.Now.Date;
        public List<ProductOrder> ProductList { get; } = productList;
    }
}