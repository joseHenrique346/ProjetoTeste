using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order.Reports.Outputs;

[method: JsonConstructor]
public class OutputMaxSaleValueBrand(string brandName, decimal totalValue, long? brandId, long quantity)
{
    public string BrandName { get; set; } = brandName;
    public long? BrandId { get; private set; } = brandId;
    public decimal TotalValue { get; private set; } = totalValue;
    public long Quantity { get; private set; } = quantity;
}