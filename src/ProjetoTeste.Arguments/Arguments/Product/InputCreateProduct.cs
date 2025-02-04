using ProjetoTeste.Arguments.Arguments.Base.Inputs;
using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Product
{
    [method: JsonConstructor]
    public class InputCreateProduct(string name, string description, string code, decimal price, long brandId, long stock) : BaseInputCreate<InputCreateProduct>
    {
        public string Name { get; } = name;
        public string Description { get; } = description;
        public string Code { get; } = code;
        public decimal Price { get; } = price;
        public long BrandId { get; set; } = brandId;
        public long Stock { get; } = stock;
    }
}