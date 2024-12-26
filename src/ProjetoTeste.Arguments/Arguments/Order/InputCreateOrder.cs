using ProjetoTeste.Arguments.Arguments.DTOs;
using System.Text.Json.Serialization;


namespace ProjetoTeste.Arguments.Arguments.Order
{
    [method: JsonConstructor]
    public class InputCreateOrder(long clientId, List<ProductOrderDto> productList)
    {
        public long ClientId { get; } = clientId;
        public List<ProductOrderDto> ProductOrder { get; } = productList;

        [JsonIgnore]
        public DateTime CreatedDate { get; } = DateTime.Now.Date;
    }
}