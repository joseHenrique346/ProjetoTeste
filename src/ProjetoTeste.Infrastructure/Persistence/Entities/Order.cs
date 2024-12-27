using System.Text.Json.Serialization;

namespace ProjetoTeste.Infrastructure.Persistence.Entities
{
    public class Order : BaseEntity
    {
        public long CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual List<ProductOrder> ProductOrders { get; set; }

        public virtual Customer Customer { get; set; }
    }
}