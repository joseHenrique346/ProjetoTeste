using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Infrastructure.Persistence.Entities
{
    [Table("produtos")]
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public long? BrandId { get; set; }
        public decimal Price { get; set; }
        public long Stock { get; set; }

        [JsonIgnore]
        public virtual Brand? Brand { get; set; }
    }
}
