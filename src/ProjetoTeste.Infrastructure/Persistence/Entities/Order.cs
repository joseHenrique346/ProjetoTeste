using ProjetoTeste.Arguments.Arguments.DTOs;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Infrastructure.Persistence.Entities
{
    public class Order : BaseEntity
    {
        public long ClientId { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual List<ProductOrder> ProductOrders { get; set; }

        [JsonIgnore]
        public virtual Client Client { get; set; }
    }
}
