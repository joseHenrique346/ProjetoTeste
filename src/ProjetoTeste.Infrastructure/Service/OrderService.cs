using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Service
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }
        public async Task<List<Order?>> GetAll()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Response<Order?>> Get(long id)
        {
            var orderId = await _orderRepository.GetAsync(id);
            return new Response<Order?> { Success = true, Request = orderId };
        }

        public async Task<Response<string?>> ValidateCreateOrderAsync(InputCreateOrder input)
        {
            var existingCustomer = await _customerRepository.GetAsync(input.CustomerId);

            if(existingCustomer is null)
            {
                return new Response<string?>  { Success = false, Message = "Não foi encontrado um cliente com este ID." };
            }

            return new Response<string?> { Success = true };
        }

        public async Task<Response<Order?>> Create(InputCreateOrder input)
        {
            var result = await ValidateCreateOrderAsync(input);

            if (!result.Success)
            {
                return new Response<Order?> { Success = false, Message = result.Message };
            }

            var order = new Order { CustomerId = input.CustomerId };

            return new Response<Order?> { Success = true, Request = order };
        }
    }
}