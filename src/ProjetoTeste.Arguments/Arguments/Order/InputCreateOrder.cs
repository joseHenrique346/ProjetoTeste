using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order
{
    public class InputCreateOrder
    {
        [JsonConstructor]
        public InputCreateOrder(long customerId)
        {
            CustomerId = customerId;
        }

        public long CustomerId { get; }

        public DateOnly CreatedDate { get; } = DateOnly.FromDateTime(DateTime.Now);
    }
}