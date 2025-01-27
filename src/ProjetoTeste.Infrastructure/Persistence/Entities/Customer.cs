using System.Text.Json.Serialization;

namespace ProjetoTeste.Infrastructure.Persistence.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }

        public Customer(string name, string email, string cpf, string phone)
        {
            Name = name;
            Email = email;
            CPF = cpf;
            Phone = phone;
        }
        public Customer()
        {

        }

        [JsonIgnore]
        public List<Order>? Order { get; set; }
    }
}