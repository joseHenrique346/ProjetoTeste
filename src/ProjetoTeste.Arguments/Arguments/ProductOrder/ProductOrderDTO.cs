using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments
{
    public class ProductOrderDTO
    {
        public ProductOrderDTO(long orderId, long productId, int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }

        public ProductOrderDTO() { }

        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }

        [JsonIgnore]
        public virtual OrderDTO Order { get; set; }
        [JsonIgnore]
        public virtual ProductDTO Product { get; set; }
    }
}
