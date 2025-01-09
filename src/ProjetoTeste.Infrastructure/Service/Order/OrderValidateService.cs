using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;

namespace ProjetoTeste.Infrastructure.Service;

public class OrderValidateService : IOrderValidateService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductOrderRepository _productOrderRepository;
    public OrderValidateService(ICustomerRepository customerRepository, IOrderRepository orderRepository, IProductRepository productRepository, IProductOrderRepository productOrderRepository)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _productOrderRepository = productOrderRepository;
    }
    public async Task<Response<InputCreateOrder?>> ValidateCreateOrder(InputCreateOrder input)
    {
        var existingCustomer = await _customerRepository.GetAsync(input.CustomerId);

        if (existingCustomer is null)
        {
            return new Response<InputCreateOrder?> { Success = false, Message = "Não foi encontrado um cliente com este ID." };
        }

        return new Response<InputCreateOrder?> { Success = true, Request = input };
    }

    public async Task<Response<InputCreateProductOrder?>> ValidateCreateProductOrder(InputCreateProductOrder input)
    {
        var existingOrder = await _orderRepository.GetAsync(input.OrderId);
        if (existingOrder is null)
        {
            return new Response<InputCreateProductOrder?> { Success = false, Message = "Id de Order inválido." };
        }

        var existingProduct = await _productRepository.GetAsync(input.ProductId);
        if (existingProduct is null)
        {
            return new Response<InputCreateProductOrder?> { Success = false, Message = "Id de Produto inválido." };
        }

        var validStock = await _productRepository.GetAsync(input.ProductId);
        if (validStock.Stock < input.Quantity)
        {
            return new Response<InputCreateProductOrder?> { Success = false, Message = "*ERRO* O Estoque não é suficiente para o pedido" };
        }

        return new Response<InputCreateProductOrder?> { Success = true };
    }
}