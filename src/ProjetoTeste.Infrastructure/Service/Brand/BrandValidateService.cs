using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Service;
using System.Xml.Linq;

namespace ProjetoTeste.Infrastructure.Service;

public class BrandValidateService : IBrandValidateService
{
    #region Validate Create

    public async Task<BaseResponse<List<BrandValidate?>>> ValidateCreateBrand(List<BrandValidate> listInputCreateBrand)
    {
        var response = new BaseResponse<List<BrandValidate?>>();

        _ = (from i in listInputCreateBrand
             where i.RepeatedCode != null
             let name = i.InputCreateBrand.Name
             let code = i.InputCreateBrand.Code
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar a marca: {name}, o código: {code} foi digitado mais de uma vez na requisição.")
             select i).ToList();

        _ = (from i in listInputCreateBrand
             where i.ExistingCodeId != null
             let name = i.InputCreateBrand.Name
             let code = i.InputCreateBrand.Code
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar a marca: {name}, o código: {code} já está sendo usado.")
             select i).ToList();

        _ = (from i in listInputCreateBrand.Select((value, index) => new { Value = value, Index = index })
             let name = i.Value.InputCreateBrand.Name
             where string.IsNullOrWhiteSpace(name) || name.Length > 40
             let index = i.Index + 1
             let setInvalid = i.Value.SetInvalid()
             let message = response.AddErrorMessage(string.IsNullOrWhiteSpace(name)
                ? $"Não foi possível criar a marca na {index}° posição, o nome não foi preenchido corretamente."
                : $"Não foi possível criar a marca: {name}, o nome ultrapassa o limite de 40 caracteres.")
             select i).ToList();

        _ = (from i in listInputCreateBrand
             let code = i.InputCreateBrand.Code
             where string.IsNullOrWhiteSpace(code) || code.Length > 6
             let name = i.InputCreateBrand.Name
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(string.IsNullOrWhiteSpace(code)
                ? $"Não foi possível criar a marca: {name}, o código não foi preenchido corretamente"
                : $"Não foi possível criar a marca: {name}, o código passa do limite de 6 caracteres")
             select i).ToList();

        _ = (from i in listInputCreateBrand
             let description = i.InputCreateBrand.Description
             where string.IsNullOrWhiteSpace(description) || description.Length > 100
             let name = i.InputCreateBrand.Name
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(string.IsNullOrWhiteSpace(description)
                ? $"Não foi possível criar a marca: {name}, a descrição não foi preenchida corretamente"
                : $"Não foi possível criar a marca: {name}, a descrição ultrapassa o limite de 100 caracteres")
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

    public async Task<BaseResponse<List<BrandValidate?>>> ValidateUpdateBrand(List<BrandValidate> listInputIdentityUpdateBrand)
    {
        var response = new BaseResponse<List<BrandValidate?>>();

        _ = (from i in listInputIdentityUpdateBrand
             where i.CurrentBrand == 0
             let name = i.InputIdentityUpdateBrand.InputUpdateBrand.Name
             let id = i.InputIdentityUpdateBrand.Id
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível atualizar a marca: {name}, o id: {id} é inválido")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateBrand
             where i.RepeatedId != 0
             let name = i.InputIdentityUpdateBrand.InputUpdateBrand.Name
             let id = i.InputIdentityUpdateBrand.Id
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível atualizar a marca: {name}, o id: {id} foi digitado mais de uma vez na requisição.")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateBrand
             where i.RepeatedCode != null
             let name = i.InputIdentityUpdateBrand.InputUpdateBrand.Name
             let code = i.InputIdentityUpdateBrand.InputUpdateBrand.Code
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível atualizar a marca: {name}, o código: {code} foi digitado mais de uma vez na requisição.")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateBrand
             where i.ExistingCodeBrand != null && i.InputIdentityUpdateBrand.Id != i.ExistingCodeBrand.Id
             let name = i.InputIdentityUpdateBrand.InputUpdateBrand.Name
             let code = i.InputIdentityUpdateBrand.InputUpdateBrand.Code
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível atualizar a marca: {name}, o código: {code} já está sendo usado.")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateBrand.Select((value, index) => new { Value = value, Index = index })
             let name = i.Value.InputIdentityUpdateBrand.InputUpdateBrand.Name
             where string.IsNullOrWhiteSpace(name) || name?.Length > 40
             let index = i.Index + 1
             let setInvalid = i.Value.SetInvalid()
             let message = response.AddErrorMessage(string.IsNullOrWhiteSpace(name)
                ? $"Não foi possível atualizar a marca na {index}° posição, o nome não foi preenchido corretamente."
                : $"Não foi possível atualizar a marca: {name}, o nome passa do limite de 40 caracteres")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateBrand
             let code = i.InputIdentityUpdateBrand.InputUpdateBrand.Code
             where string.IsNullOrWhiteSpace(code) || code?.Length > 6
             let name = i.InputIdentityUpdateBrand.InputUpdateBrand.Name
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(string.IsNullOrWhiteSpace(code)
                ? $"Não foi possível atualizar a marca: {name}, o código não foi preenchido corretamente."
                : $"Não foi possível atualizar a marca: {name}, o código ultrapassa o limite de 6 caracteres")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateBrand
             let description = i.InputIdentityUpdateBrand.InputUpdateBrand.Description
             where string.IsNullOrWhiteSpace(description) || description?.Length > 100
             let name = i.InputIdentityUpdateBrand.InputUpdateBrand.Name
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(string.IsNullOrWhiteSpace(description)
                ? $"Não foi possível atualizar a marca: {name}, a descrição não foi preenchida corretamente."
                : $"Não foi possível atualizar a marca: {name}, a descrição passa do limite de 100 caracteres")
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
             where i.RepeatedId != null
             let id = i.InputIdentityDeleteBrand.Id
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível deletar a marca, {id} é digitado mais de uma vez na requisição")
             select i).ToList();

        _ = (from i in listInputIdentityDeleteBrand
             where i.ExistingBrand == 0
             let id = i.InputIdentityDeleteBrand.Id
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível deletar a marca, {id} é inválido")
             select i).ToList();

        _ = (from i in listInputIdentityDeleteBrand
             where i.ExistingProductInBrand != 0
             let id = i.InputIdentityDeleteBrand.Id
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível deletar a marca com o Id: {id}, existem produtos cadastrados nele")
             select i).ToList();

        var selecteListValidBrand = (from i in listInputIdentityDeleteBrand
                                     where !i.Invalid
                                     select i).ToList();

        if (!selecteListValidBrand.Any())
        {
            response.Success = false;
        }

        response.Content = selecteListValidBrand;
        return response;
    }
}

#endregion