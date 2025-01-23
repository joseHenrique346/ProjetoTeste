using ProjetoTeste.Arguments.Arguments;

namespace ProjetoTeste.Arguments.Arguments
{
    public class OrderDTO
    {
        public OrderDTO() { }

        public OrderDTO(long customerId, DateTime createdDate)
        {
            CustomerId = customerId;
            CreatedDate = createdDate;
        }

        public long CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Total { get; set; }

        public List<ProductOrderDTO> ListProductOrder { get; set; } = new List<ProductOrderDTO> { };
        public CustomerDTO? Customer { get; set; }
    }
}
