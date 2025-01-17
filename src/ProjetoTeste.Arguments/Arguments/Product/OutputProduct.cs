using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Product
{
    [method: JsonConstructor]
    public class OutputProduct(long id, string name, string description, string code, decimal price, long? brandId, long stock)
    {
        public long Id { get; } = id;
        public string Name { get; } = name;
        public string Description { get; } = description;
        public string Code { get; } = code;
        public decimal Price { get; } = price;
        public long? BrandId { get; set; } = brandId;
        public long Stock { get; } = stock;
    }
}