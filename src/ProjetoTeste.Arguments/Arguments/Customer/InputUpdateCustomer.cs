using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Customer
{
    [method: JsonConstructor]
    public class InputUpdateCustomer(long id, string name, string email, string cpf, string phone)
    {
        public long Id { get; } = id;
        public string Name { get; } = name;
        public string Email { get; } = email;
        public string CPF { get; } = cpf;
        public string Phone { get; } = phone;
    }
}