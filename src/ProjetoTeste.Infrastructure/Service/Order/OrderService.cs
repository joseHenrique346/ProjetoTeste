using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Service
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderValidateService _orderValidateService;
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IProductOrderRepository productOrderRepository, IOrderValidateService orderValidateService)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _productOrderRepository = productOrderRepository;
            _orderValidateService = orderValidateService;
        }
        public async Task<List<Order>> GetAll()
        {
            return await _orderRepository.GetWithIncludesAsync();
        }

        public async Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct()
        {
            var higherOrders = await _orderRepository.GetMostOrderedProduct();
            return higherOrders;
        } 

        //public async Task<Order> GetOrderAveragePrice()
        //{

        //}

        //public async Task<Order> LowerOrderProduct()
        //{

        //}

        //public async Task<Order> GetHigherOrderBrand()
        //{

        //}

        //public async Task<Order> GetHigherOrderClient()
        //{

        //}

        //public async Task<Order> GetLowerOrderClient()
        //{

        //}

        public async Task<Response<List<Order>?>> Get(long id)
        {
            var orderId = await _orderRepository.GetWithIncludesAsync(id);
            return new Response<List<Order?>> { Success = true, Request = orderId };
        }

        public async Task<Response<Order>> Create(InputCreateOrder input)
        {
            var result = await _orderValidateService.ValidateCreateOrder(input);

            if (!result.Success)
            {
                return new Response<Order> { Success = false, Message = result.Message };
            }
            var order = new Order { CustomerId = input.CustomerId };

            await _orderRepository.CreateAsync(order);
            return new Response<Order> { Success = true, Request = order };
        }
        public async Task<Response<OutputProductOrder>> CreateProductOrder(InputCreateProductOrder input)
        {
            var result = await _orderValidateService.ValidateCreateProductOrder(input);
            if (!result.Success)
            {
                return new Response<OutputProductOrder> { Success = false, Message = result.Message };
            }

            var currentProduct = await _productRepository.GetAsync(input.ProductId);

            var createProductOrder = new ProductOrder()
            {
                OrderId = input.OrderId,
                ProductId = input.ProductId,
                Quantity = input.Quantity,
                UnitPrice = currentProduct.Price,
                TotalPrice = input.Quantity * currentProduct.Stock
            };

            //var createProductOrder = new ProductOrder() { OrderId = input.OrderId, ProductId = input.ProductId, Quantity = input.Quantity, UnitPrice = currentProduct.Price };
            await _productOrderRepository.CreateAsync(createProductOrder);

            currentProduct.Stock -= input.Quantity;
            await _productRepository.Update(currentProduct);

            return new Response<OutputProductOrder> { Success = true, Request = createProductOrder.ToOutputProductOrder() };
        }
    }
}