namespace ProjetoTeste.Infrastructure.Persistence.Entities
{
    public class ProductOrder : BaseEntity
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
