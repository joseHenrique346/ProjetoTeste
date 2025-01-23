using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments
{
    public class CustomerDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Phone { get; set; }

        public CustomerDTO(string name, string email, string cpf, string phone)
        {
            Name = name;
            Email = email;
            CPF = cpf;
            Phone = phone;
        }
        public CustomerDTO()
        {

        }

        [JsonIgnore]
        public List<OrderDTO>? Order { get; set; }
    }
}
