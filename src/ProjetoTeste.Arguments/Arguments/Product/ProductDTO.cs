using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments
{
    public class ProductDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public long BrandId { get; set; }
        public decimal Price { get; set; }
        public long Stock { get; set; }

        public ProductDTO(long id, string name, string code, string description, long brandId, decimal price, long stock)
        {
            Id = id;
            Name = name;
            Code = code;
            Description = description;
            BrandId = brandId;
            Price = price;
            Stock = stock;
        }

        public ProductDTO()
        {

        }

        public List<ProductOrderDTO>? ListProductOrder { get; set; }

        [JsonIgnore]
        public virtual BrandDTO? Brand { get; set; }
    }
}
