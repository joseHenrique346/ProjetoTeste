using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.Order.GetLINQ;
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
        public async Task<List<OutputOrder>> GetAll()
        {
            var get = await _orderRepository.GetWithIncludesAsync();
            return get.Select(i => i.ToOutputOrder()).ToList();
        }

        public async Task<List<OutputMaxSaleValueProduct>> GetMostOrderedProduct()
        {
            var higherOrders = await _orderRepository.GetMostOrderedProduct();
            return higherOrders;
        }

        public async Task<OutputAverageSaleValueOrder> GetOrderAveragePrice()
        {
            var averageOrder = await _orderRepository.GetOrderAveragePrice();
            return averageOrder;
        }

        public async Task<List<OutputMinSaleValueProduct>> LeastOrderedProduct()
        {
            var leastOrders = await _orderRepository.GetLeastOrderedProduct();
            return leastOrders;
        }

        //public async Task<OutputMaxSaleValueBrand> GetMostOrderedBrand()
        //{
        //    var mostOrderedBrand = await _orderRepository.GetMostOrderedBrand();
            
        //    mostOrderedBrand.BrandName = $"A Marca mais vendida é: {}";
        //    return mostOrderedBrand;
        //}

        //public async Task<Order> GetHigherOrderClient()
        //{

        //}

        //public async Task<Order> GetLowerOrderClient()
        //{

        //}

        public async Task<List<OutputOrder>?> Get(long id)
        {
            var orderId = await _orderRepository.GetWithIncludesAsync(id);
            return orderId.Select(i => i.ToOutputOrder()).ToList();
        }

        public async Task<Response<OutputOrder>> Create(InputCreateOrder input)
        {
            var result = await _orderValidateService.ValidateCreateOrder(input);

            if (!result.Success)
            {
                return new Response<OutputOrder> { Success = false, Message = result.Message };
            }
            var order = new Order { CustomerId = input.CustomerId };

            await _orderRepository.CreateAsync(order);
            return new Response<OutputOrder> { Success = true, Request = order.ToOutputOrder() };
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
                SubTotal = input.Quantity * currentProduct.Price
            };

            //var createProductOrder = new ProductOrder() { OrderId = input.OrderId, ProductId = input.ProductId, Quantity = input.Quantity, UnitPrice = currentProduct.Price };
            await _productOrderRepository.CreateAsync(createProductOrder);

            currentProduct.Stock -= input.Quantity;
            await _productRepository.Update(currentProduct);

            return new Response<OutputProductOrder> { Success = true, Request = createProductOrder.ToOuputProductOrder() };
        }
    }
}