using ProjetoTeste.Infrastructure.Core.Entities;

namespace ProjetoTeste.Infrastructure.Service
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<Order?>> GetAllOrderAsync()
        {
            var AllOrder = await _orderRepository.GetAllAsync();
            return AllOrder;
        }
    }
}
