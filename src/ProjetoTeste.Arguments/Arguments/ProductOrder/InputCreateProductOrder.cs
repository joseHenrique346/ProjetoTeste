namespace ProjetoTeste.Arguments.Arguments.ProductOrder
{
    public class InputCreateProductOrder(long orderId, long productId, int quantity)
    {
        public long OrderId { get; } = orderId;
        public long ProductId { get; } = productId;
        public int Quantity { get; } = quantity;
    }
}