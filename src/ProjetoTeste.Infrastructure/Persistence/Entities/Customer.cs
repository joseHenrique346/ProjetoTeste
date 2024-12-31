using System.Text.Json.Serialization;

namespace ProjetoTeste.Infrastructure.Persistence.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }

        [JsonIgnore]
        public List<Order>? Order { get; set; }

        //public string Role { get; set; }
        //public string PasswordHash { get; set; }
    }
}