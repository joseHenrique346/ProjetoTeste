public class MinSaleValueProductDTO
{
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public string ProductDescription { get; set; }
    public long? ProductBrandId { get; set; }
    public long TotalSaleQuantity { get; set; }
    public decimal TotalSaleValue { get; set; }
}
