using System.Text.Json.Serialization;

namespace ProjetoTeste.Infrastructure.Persistence.Entities
{
    public class ProductOrder : BaseEntity
    {
        public ProductOrder(long orderId, long productId, int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
        public ProductOrder() { }

        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}