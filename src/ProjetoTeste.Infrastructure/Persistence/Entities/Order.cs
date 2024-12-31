using System.Text.Json.Serialization;

namespace ProjetoTeste.Infrastructure.Persistence.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            CreatedDate = DateOnly.FromDateTime(DateTime.Now);
        }
        public long CustomerId { get; set; }
        public DateOnly CreatedDate { get; set; }

        [JsonIgnore]
        public virtual List<ProductOrder> ProductOrders { get; set; }
        [JsonIgnore]
        public virtual Customer Customer { get; set; }
    }
}