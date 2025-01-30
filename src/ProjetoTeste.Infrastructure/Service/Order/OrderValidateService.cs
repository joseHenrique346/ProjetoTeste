using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Service;

namespace ProjetoTeste.Infrastructure.Service;

public class OrderValidateService : IOrderValidateService
{
    #region Validate Create Order

    public async Task<BaseResponse<List<OrderValidate?>>> ValidateCreateOrder(List<OrderValidate> listInputCreateOrder)
    {
        var response = new BaseResponse<List<OrderValidate?>>();

        _ = (from i in listInputCreateOrder
             where i.ExistingCustomer == 0
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar o pedido, o id {i.InputCreateOrder.CustomerId} do cliente é inválido")
             select i).ToList();

        var selectedValidList = (from i in listInputCreateOrder
                                 where !i.Invalid
                                 select i).ToList();

        if (!selectedValidList.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = selectedValidList;
        return response;
    }

    #endregion

    #region Validate Create ProductOrder
    public async Task<BaseResponse<List<ProductOrderValidate?>>> ValidateCreateProductOrder(List<ProductOrderValidate> listInputCreateProductOrder)
    {
        var response = new BaseResponse<List<ProductOrderValidate>>();

        _ = (from i in listInputCreateProductOrder.Select((value, index) => new { Value = value, Index = index })
             where i.Value.ExistingOrder == 0
             let setInvalid = i.Value.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível fazer o {i.Index + 1}º pedido, seu id {i.Value.InputCreateProductOrder.OrderId} é inválido")
             select i.Value).ToList();

        _ = (from i in listInputCreateProductOrder.Select((value, index) => new { Value = value, Index = index })
             where i.Value.ExistingProduct == null
             let setInvalid = i.Value.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível fazer o {i.Index + 1}º pedido, o id de produto {i.Value.InputCreateProductOrder.ProductId} é inválido")
             select i.Value).ToList();

        _ = (from i in listInputCreateProductOrder.Select((value, index) => new { Value = value, Index = index })
             where i.Value.InputCreateProductOrder.Quantity <= 0
             let setInvalid = i.Value.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível fazer o {i.Index + 1}º pedido, a quantidade é inválida")
             select i.Value).ToList();

        var listProduct = listInputCreateProductOrder.Select(i => i.ExistingProduct).ToList();


        foreach (var i in listInputCreateProductOrder.Select((value, index) => new { Value = value, Index = index }))
        {
            if (!i.Value.Invalid && listProduct.FirstOrDefault(k => k.Id == i.Value.InputCreateProductOrder.ProductId).Stock > i.Value.InputCreateProductOrder.Quantity)
                i.Value.ExistingProduct.Stock -= i.Value.InputCreateProductOrder.Quantity;
            else
                i.Value.SetInvalid();
            response.AddErrorMessage($"Não foi possível fazer o {i.Index + 1}º pedido, o estoque não é suficiente");
        }

        //_ = (from i in listInputCreateProductOrder.Select((value, index) => new { Value = value, Index = index })
        //     where !i.Value.Invalid && i.Value.ExistingProduct.Stock > i.Value.InputCreateProductOrder.Quantity
        //     let updateStock = i.Value.ExistingProduct.Stock -= i.Value.InputCreateProductOrder.Quantity
        //     select i.Value).ToList();

        //_ = (from i in listInputCreateProductOrder.Select((value, index) => new { Value = value, Index = index })
        //     where i.Value.ExistingProduct?.Stock < i.Value.InputCreateProductOrder?.Quantity
        //     let setInvalid = i.Value.SetInvalid()
        //     let message = response.AddErrorMessage($"Não foi possível fazer o {i.Index + 1}º pedido, o estoque não é suficiente")
        //     select i.Value).ToList();

        var selectedValidList = (from i in listInputCreateProductOrder
                                 where !i.Invalid
                                 select i).ToList();

        if (!selectedValidList.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = selectedValidList;
        return response;
    }
}

#endregion