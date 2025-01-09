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

        public List<ProductOrder> ListProductOrder { get; set; }
        public Customer? Customer { get; set; }
    }
}