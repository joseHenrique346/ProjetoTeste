using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;

namespace ProjetoTeste.Infrastructure.Service;

public class BrandValidateService : IBrandValidateService
{

    #region Dependency Injection

    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    public BrandValidateService(IBrandRepository brandRepository, IProductRepository productRepository)
    {
        _brandRepository = brandRepository;
        _productRepository = productRepository;
    }

    #endregion

    #region Validate Create

    public async Task<BaseResponse<List<BrandValidate?>>> ValidateCreateBrand(List<BrandValidate> listInputCreateBrand)
    {
        var response = new BaseResponse<List<BrandValidate?>>();

        _ = (from i in listInputCreateBrand
             where i.RepeatedCode != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar a marca: {i.InputCreateBrand.Name}, foi digitado mais de uma vez na requisição.")
             select i).ToList();

        _ = (from i in listInputCreateBrand
             where i.ExistingCode != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar a marca: {i.InputCreateBrand.Name}, o código: \"{i.InputCreateBrand.Code}\" já está sendo usado.")
             select i).ToList();

        _ = (from i in listInputCreateBrand
             where i.InputCreateBrand.Name.Length > 40 || string.IsNullOrEmpty(i.InputCreateBrand.Name) || string.IsNullOrWhiteSpace(i.InputCreateBrand.Name)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputCreateBrand.Name.Length > 40 ? $"Não foi possível criar a marca: {i.InputCreateBrand.Name}, o nome passa do limite de 40 caracteres" : $"Não foi possível criar a marca: {i.InputCreateBrand.Name}, o nome não foi preenchido corretamente.")
             select i).ToList();

        _ = (from i in listInputCreateBrand
             where i.InputCreateBrand.Code.Length > 6 || string.IsNullOrEmpty(i.InputCreateBrand.Code) || string.IsNullOrWhiteSpace(i.InputCreateBrand.Code)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputCreateBrand.Code.Length > 6 ? $"Não foi possível criar a marca: {i.InputCreateBrand.Name}, o código passa do limite de 6 caracteres" : $"Não foi possível criar a marca: {i.InputCreateBrand.Name}, o código não foi preenchido corretamente.")
             select i).ToList();

        _ = (from i in listInputCreateBrand
             where i.InputCreateBrand.Description.Length > 100 || string.IsNullOrEmpty(i.InputCreateBrand.Description) || string.IsNullOrWhiteSpace(i.InputCreateBrand.Description)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputCreateBrand.Description.Length > 100 ? $"Não foi possível criar a marca: {i.InputCreateBrand.Name}, a descrição passa do limite de 100 caracteres" : $"Não foi possível criar a marca: {i.InputCreateBrand.Name}, a descrição não foi preenchida corretamente.")
             select i).ToList();

        var selectedValidListBrand = (from i in listInputCreateBrand
                                      where !i.Invalid
                                      select i).ToList();

        if (!selectedValidListBrand.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = selectedValidListBrand;
        return response;
    }

    #endregion

    #region Validate Update

    public async Task<BaseResponse<List<InputIdentityUpdateBrand?>>> ValidateUpdateBrand(List<InputIdentityUpdateBrand> listInputIdentityUpdateBrand)
    {
        var response = new BaseResponse<List<InputIdentityUpdateBrand?>>();

        _ = (from i in listInputIdentityUpdateBrand
             where i.CurrentBrand == 0
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar a marca: {i.InputIdentityUpdateBrand.InputUpdateBrand.Name}, o \"{i.InputIdentityUpdateBrand.Id}\" é inválido")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateBrand
             where i.RepeatedCode != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar a marca: {i.InputIdentityUpdateBrand.InputUpdateBrand.Name}, \"{i.InputIdentityUpdateBrand.InputUpdateBrand.Code}\" foi digitado mais de uma vez na requisição.")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateBrand
             where i.RepeatedCode != null
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar a marca: {i.InputIdentityUpdateBrand.InputUpdateBrand.Name}, \"{i.InputIdentityUpdateBrand.Id}\"  foi digitado mais de uma vez na requisição.")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateBrand
             where i.ExistingCode != null && i.InputIdentityUpdateBrand.Id != i.CurrentBrand
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar a marca: {i.InputIdentityUpdateBrand.InputUpdateBrand.Name}, o código: \"{i.InputIdentityUpdateBrand.InputUpdateBrand.Code}\" já está sendo usado.")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateBrand
             where i.InputIdentityUpdateBrand.InputUpdateBrand.Name.Length > 40 || string.IsNullOrEmpty(i.InputIdentityUpdateBrand.InputUpdateBrand.Name) || string.IsNullOrWhiteSpace(i.InputIdentityUpdateBrand.InputUpdateBrand.Name)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputIdentityUpdateBrand.InputUpdateBrand.Name.Length > 40 ? $"Não foi possível criar a marca: {i.InputIdentityUpdateBrand.InputUpdateBrand.Name}, o nome passa do limite de 40 caracteres" : $"Não foi possível criar a marca: {i.InputCreateBrand.Name}, o nome não foi preenchido corretamente.")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateBrand
             where i.InputIdentityUpdateBrand.InputUpdateBrand.Code.Length > 6 || string.IsNullOrEmpty(i.InputIdentityUpdateBrand.InputUpdateBrand.Code) || string.IsNullOrWhiteSpace(i.InputIdentityUpdateBrand.InputUpdateBrand.Code)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputIdentityUpdateBrand.InputUpdateBrand.Code.Length > 6 ? $"Não foi possível criar a marca: {i.InputIdentityUpdateBrand.InputUpdateBrand.Name}, o código passa do limite de 6 caracteres" : $"Não foi possível criar a marca: {i.InputIdentityUpdateBrand.InputUpdateBrand.Name}, o código não foi preenchido corretamente.")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateBrand
             where i.InputIdentityUpdateBrand.InputUpdateBrand.Description.Length > 100 || string.IsNullOrEmpty(i.InputIdentityUpdateBrand.InputUpdateBrand.Description) || string.IsNullOrWhiteSpace(i.InputIdentityUpdateBrand.InputUpdateBrand.Description)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputIdentityUpdateBrand.InputUpdateBrand.Description.Length > 100 ? $"Não foi possível criar a marca: {i.InputIdentityUpdateBrand.InputUpdateBrand.Name}, a descrição passa do limite de 100 caracteres" : $"Não foi possível criar a marca: {i.InputIdentityUpdateBrand.InputUpdateBrand.Name}, a descrição não foi preenchida corretamente.")
             select i).ToList();

        var selectedValidListBrand = (from i in listInputIdentityUpdateBrand
                                      where !i.Invalid
                                      select i).ToList();

        if (!selectedValidListBrand.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = selectedValidListBrand;
        return response;
    }

    #endregion

    #region Validate Delete

    public async Task<BaseResponse<List<BrandValidate?>>> ValidateDeleteBrand(List<BrandValidate> listInputIdentityDeleteBrand)
    {
        var response = new BaseResponse<List<BrandValidate?>>();

        _ = (from i in listInputIdentityDeleteBrand
             where i.ExistingBrand == 0
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível deletar a marca, {i.InputIdentityDeleteBrand.Id} é inválido")
             select i).ToList();

        _ = (from i in listInputIdentityDeleteBrand
             where i.ExistingProductInBrand != 0
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível deletar a marca com o Id: {i.InputIdentityDeleteBrand.Id}, existem produtos cadastrados nele")
             select i).ToList();

        if (!listInputIdentityDeleteBrand.Any())
        {
            response.Success = false;
        }

        var selecteListValidBrand = (from i in listInputIdentityDeleteBrand
                                     where !i.Invalid
                                     select i).ToList();

        response.Content = selecteListValidBrand;
        return response;
    }
}

#endregion