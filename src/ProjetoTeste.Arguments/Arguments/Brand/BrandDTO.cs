using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments
{
    public class BrandDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public BrandDTO(long id, string name, string code, string description)
        {
            Id = id;
            Name = name;
            Code = code;
            Description = description;
        }

        public BrandDTO()
        {

        }
        [JsonIgnore]
        public virtual List<ProductDTO>? ListProduct { get; set; }
    }
}
