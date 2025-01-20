using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;

namespace ProjetoTeste.Infrastructure.Service;

public class OrderValidateService : IOrderValidateService
{
    #region Dependency Injection

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

    #endregion

    #region Validate Create Order

    public async Task<BaseResponse<InputCreateOrder?>> ValidateCreateOrder(InputCreateOrder input)
    {
        var response = new BaseResponse<InputCreateOrder?>();

        var existingCustomer = await _customerRepository.GetListByListId(input.CustomerId);

        if (existingCustomer is null)
            response.AddErrorMessage("Não foi encontrado um cliente com este ID.");

        if (response.Message.Count > 0)
            response.Success = false;

        return response;
    }

    #endregion

    #region Validate Create ProductOrder
    public async Task<BaseResponse<InputCreateProductOrder?>> ValidateCreateProductOrder(InputCreateProductOrder input)
    {
        var response = new BaseResponse<InputCreateProductOrder>();

        var existingOrder = await _orderRepository.GetListByListId(input.OrderId);
        if (existingOrder is null)
            response.AddErrorMessage("Id de Order inválido.");

        var existingProduct = await _productRepository.GetListByListId(input.ProductId);
        if (existingProduct is null)
            response.AddErrorMessage("Id de Produto inválido.");

        var currentProduct = await _productRepository.GetListByListId(input.ProductId);
        if (currentProduct.Stock < input.Quantity)
            response.AddErrorMessage("O Estoque não é suficiente para o pedido.");

        if (input.Quantity <= 0)
            response.AddErrorMessage("Quantidade inválida para pedido.");

        if (response.Message.Count > 0)
            response.Success = false;

        return response;
    }

    #endregion
}