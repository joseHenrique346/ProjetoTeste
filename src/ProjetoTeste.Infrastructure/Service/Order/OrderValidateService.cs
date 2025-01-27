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

    public async Task<BaseResponse<List<OutputOrder?>>> ValidateCreateOrder(List<InputCreateOrder> listInputCreateOrder)
    {
        var response = new BaseResponse<List<OutputOrder?>>();

        var existingCustomer = await _customerRepository.GetListByListIdWhere(listInputCreateOrder.Select(i => i.CustomerId).ToList());

        for (var i = 0; i < listInputCreateOrder.Count; i++)
        {
            if (existingCustomer[i] is null)
                response.AddErrorMessage("Não foi encontrado um cliente com este ID.");

            if (response.Message.Count > 0)
            {
                response.Success = false;
                listInputCreateOrder.Remove(listInputCreateOrder[i]);
            }
        }
        return response;
    }

    #endregion

    #region Validate Create ProductOrder
    public async Task<BaseResponse<List<OutputProductOrder?>>> ValidateCreateProductOrder(List<InputCreateProductOrder> listInputCreateProductOrder)
    {
        var response = new BaseResponse<List<OutputProductOrder>>();

        for (var i = 0; i < listInputCreateProductOrder.Count; i++)
        {
            var existingOrder = await _orderRepository.GetListByListIdWhere(listInputCreateProductOrder.Select(i => i.OrderId).ToList());
            if (existingOrder[i] is null)
                response.AddErrorMessage("Id de Order inválido.");

            var currentProduct = await _productRepository.GetListByListIdWhere(listInputCreateProductOrder.Select(i => i.ProductId).ToList());
            if (currentProduct[i] is null)
                response.AddErrorMessage("Id de Produto inválido.");

            if (currentProduct[i].Stock < listInputCreateProductOrder[i].Quantity)
                response.AddErrorMessage("O Estoque não é suficiente para o pedido.");

            if (listInputCreateProductOrder[i].Quantity <= 0)
                response.AddErrorMessage("Quantidade inválida para pedido.");

            if (response.Message.Count > 0)
            {
                response.Success = false;
                listInputCreateProductOrder.Remove(listInputCreateProductOrder[i]);
            }
        }
        return response;
    }
}

#endregion