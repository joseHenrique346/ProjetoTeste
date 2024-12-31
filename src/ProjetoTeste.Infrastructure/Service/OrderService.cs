using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Service
{
    public class OrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IProductRepository _productRepository;
        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, IProductOrderRepository productOrderRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _productOrderRepository = productOrderRepository;
        }
        public async Task<List<Order>> GetAll()
        {
            await _unitOfWork.BeginTransactionAsync();
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Response<Order?>> Get(long id)
        {
            await _unitOfWork.BeginTransactionAsync();
            var orderId = await _orderRepository.GetAsync(id);
            return new Response<Order?> { Success = true, Request = orderId };
        }

        public async Task<Response<string?>> ValidateCreateOrder(InputCreateOrder input)
        {
            await _unitOfWork.BeginTransactionAsync();
            var existingCustomer = await _customerRepository.GetAsync(input.CustomerId);

            if (existingCustomer is null)
            {
                return new Response<string?> { Success = false, Message = "Não foi encontrado um cliente com este ID." };
            }

            return new Response<string?> { Success = true };
        }

        public async Task<Response<Order>> Create(InputCreateOrder input)
        {
            var result = await ValidateCreateOrder(input);

            if (!result.Success)
            {
                return new Response<Order> { Success = false, Message = result.Message };
            }
            var order = new Order { CustomerId = input.CustomerId };

            await _orderRepository.CreateAsync(order);
            return new Response<Order> { Success = true, Request = order };
        }

        public async Task<Response<string?>> ValidateCreateProductOrder(InputCreateProductOrder input)
        {
            var existingOrder = await _orderRepository.GetAsync(input.OrderId);
            if (existingOrder is null)
            {
                return new Response<string?> { Success = false, Message = "Id de Order inválido." };
            }

            var existingProduct = await _productRepository.GetAsync(input.ProductId);
            if (existingProduct is null)
            {
                return new Response<string?> { Success = false, Message = "Id de Produto inválido." };
            }

            try
            {
                var existingProductOrder = await _productOrderRepository.GetAsync(input.OrderId, input.ProductId);
            }
            catch (Exception ex)
            {
                throw;
            }
            if (existingProduct != null)
            {
                return new Response<string?> { Success = false, Message = "Já existe um pedido com este Produto." };
            }

            var validStock = await _productRepository.GetAsync(input.ProductId);
            if (validStock.Stock < input.Quantity)
            {
                return new Response<string?> { Success = false, Message = "*ERRO* O Estoque não é suficiente para o pedido" };
            }

            return new Response<string?> { Success = true };
        }

        public async Task<Response<OutputProductOrder>> CreateProductOrder(InputCreateProductOrder input)
        {
            var result = await ValidateCreateProductOrder(input);
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
                UnitPrice = currentProduct.Price
            };

            //var createProductOrder = new ProductOrder() { OrderId = input.OrderId, ProductId = input.ProductId, Quantity = input.Quantity, UnitPrice = currentProduct.Price };
            await _productOrderRepository.CreateAsync(createProductOrder);

            currentProduct.Stock -= input.Quantity;
            await _productRepository.Update(currentProduct);

            return new Response<OutputProductOrder> { Success = true, Request = createProductOrder.ToOutputProductOrder() };
        }
    }
}