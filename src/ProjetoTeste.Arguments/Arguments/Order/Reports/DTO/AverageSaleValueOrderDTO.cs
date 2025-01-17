namespace ProjetoTeste.Arguments.Arguments.Order.Reports.DTO
{
    public class AverageSaleValueOrderDTO
    {
        public long OrderId { get; set; }
        public int TotalSaleQuantity { get; set; }
        public decimal Total { get; set; }
        public decimal AverageTotalSaleValue { get; set; }
    }
}