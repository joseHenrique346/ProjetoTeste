namespace ProjetoTeste.Infrastructure.Persistence.Entities
{
    public class Order : BaseEntity
    {
        public Order() { }

        public Order(long customerId, DateTime createdDate)
        {
            CustomerId = customerId;
            CreatedDate = createdDate;
        }

        public long CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Total { get; set; }

        public List<ProductOrder> ListProductOrder { get; set; } = new List<ProductOrder> { };
        public Customer? Customer { get; set; }
    }
}