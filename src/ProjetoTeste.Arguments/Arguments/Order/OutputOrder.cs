using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order
{
    public class OutputOrder
    {
        [JsonConstructor]
        public OutputOrder(long id, long customerId)
        {
            Id = id;
            CustomerId = customerId;
            CreatedDate = DateOnly.FromDateTime(DateTime.Now);
        }

        public long Id { get; set; }
        public long CustomerId { get; }

        public DateOnly CreatedDate { get; }
    }
}