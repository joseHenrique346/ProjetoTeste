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
        public long BrandId { get; set; } 
        public decimal Price { get; set; } 
        public long Stock { get; set; }

        public Product(string name, string code, string description, long brandId, decimal price, long stock)
        {
            Name = name;
            Code = code;
            Description = description;
            BrandId = brandId;
            Price = price;
            Stock = stock;
        }

        public Product() 
        {
             
        }

        public List<ProductOrder>? ListProductOrder { get; set; }

        [JsonIgnore]
        public virtual Brand? Brand { get; set; }
    }
}