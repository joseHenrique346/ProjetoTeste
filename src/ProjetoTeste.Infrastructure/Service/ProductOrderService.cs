using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Service
{
    public class ProductOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public ProductOrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<Response<string?>> ValidateCreate(InputCreateProductOrder input)
        {
            var existingOrder = await _orderRepository.GetAsync(input.OrderId);
            if (existingOrder is null)
            {
                return new Response<string?> { Success = false, Message = "*ERRO* Este id de pedido não é válido, foi informado corretamente?" };
            }

            var existingProduct = await _productRepository.GetAsync(input.ProductId);
            if (existingProduct is null)
            {
                return new Response<string?> { Success = false, Message = "*ERRO* Este id de produto não é válido, foi informado corretamente?" };
            }

            var validStock = await _productRepository.GetAsync(input.ProductId);
            if (validStock.Stock < input.Quantity)
            {
                return new Response<string?> { Success = false, Message = "*ERRO* O Estoque não é suficiente para o pedido" };
            }

            return new Response<string?> { Success = true };
        }

        public async Task<Response<ProductOrder>> Create(InputCreateProductOrder input)
        {
            var result = await ValidateCreate(input);
            if (!result.Success) 
            {
                return new Response<ProductOrder> { Success = false, Message = result.Message };
            }

            var productOrder = new ProductOrder { OrderId = input.OrderId, ProductId = input.ProductId, Quantity = input.Quantity };
            return new Response<ProductOrder> { Success = true, Request = productOrder };
        }
    }
}
