using System.Text.Json.Serialization;

namespace ProjetoTeste.Infrastructure.Persistence.Entities
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Brand(string name, string code, string description)
        {
            Name = name;
            Code = code;
            Description = description;
        }
        public Brand()
        {
             
        }
        [JsonIgnore]
        public virtual List<Product>? ListProduct { get; set; }
    }
}