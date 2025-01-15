using System.Text.Json.Serialization;

namespace ProjetoTeste.Infrastructure.Persistence.Entities
{
    public class Order : BaseEntity
    {

        public Order() { }

        public Order(long customerId, DateTime createdDate, List<ProductOrder> listProductOrder, Customer? customer)
        {
            CustomerId = customerId;
            CreatedDate = createdDate;
        }

        public long CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Total { get; set; }

        public List<ProductOrder> ListProductOrder { get; set; }
        public Customer? Customer { get; set; }
    }
}