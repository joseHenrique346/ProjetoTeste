using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order.GetLINQ;

[method: JsonConstructor]
public class OutputMinSaleValueProduct(long id, string name, string code, string description, decimal totalValue, long? brandId, long quantity)
{
    public long Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Code { get; private set; } = code;
    public string Description { get; private set; } = description;
    public decimal TotalValue { get; private set; } = totalValue;
    public long? BrandId { get; private set; } = brandId;
    public long Quantity { get; private set; } = quantity;
}