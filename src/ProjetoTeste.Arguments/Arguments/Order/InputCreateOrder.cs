using ProjetoTeste.Arguments.Arguments.Base.Inputs;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order
{
    public class InputCreateOrder : BaseInputCreate<InputCreateOrder>
    {
        [JsonConstructor]
        public InputCreateOrder(long customerId)
        {
            CustomerId = customerId;
        }

        public long CustomerId { get; }

        public DateTime CreatedDate { get; } = DateTime.Now;
    }
}