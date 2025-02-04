using ProjetoTeste.Arguments.Arguments.Base.Outputs;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Customer
{
    [method: JsonConstructor]
    public class OutputCustomer(long id, string name, string email, string cpf, string phone) : BaseOutput<OutputCustomer>
    {
        public long Id { get; } = id;
        public string Name { get; } = name;
        public string Email { get; } = email;
        public string CPF { get; } = cpf;
        public string Phone { get; } = phone;
    }
}