using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Service;

namespace ProjetoTeste.Infrastructure.Service;

public class CustomerValidateService : ICustomerValidateService
{
    #region Validate Create

    public async Task<BaseResponse<List<CustomerValidate?>>> ValidateCreateCustomer(List<CustomerValidate> listInputCreateCustomer)
    {
        var response = new BaseResponse<List<CustomerValidate?>>();

        _ = (from i in listInputCreateCustomer
             where i.InputCreateCustomer.CPF?.Length != 11
             let name = i.InputCreateCustomer.Name
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar o cliente {name}, o cpf está inválido")
             select i).ToList();

        _ = (from i in listInputCreateCustomer
             where i.InputCreateCustomer.Phone?.Length != 11
             let name = i.InputCreateCustomer.Name
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar o cliente {name}, o telefone está inválido")
             select i).ToList();

        _ = (from i in listInputCreateCustomer
             where i.InputCreateCustomer.Email?.Length > 60
             let name = i.InputCreateCustomer.Name
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar o cliente {name}, o email passa do limite de 60 caracteres")
             select i).ToList();

        _ = (from i in listInputCreateCustomer
             where !i.InputCreateCustomer.Email.Contains("@")
             let name = i.InputCreateCustomer.Name
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar o cliente {name}, o email está inválido")
             select i).ToList();

        _ = (from i in listInputCreateCustomer
             where !i.InputCreateCustomer.Email.EndsWith(".com")
             let name = i.InputCreateCustomer.Name
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar o cliente {name}, o email é inválido")
             select i).ToList();

        _ = (from i in listInputCreateCustomer.Select((value, index) => new { Value = value, Index = index })
             let name = i.Value.InputCreateCustomer.Name
             where string.IsNullOrWhiteSpace(name) || name?.Length > 40
             let index = i.Index + 1
             let setInvalid = i.Value.SetInvalid()
             let message = response.AddErrorMessage(string.IsNullOrWhiteSpace(name)
                ? $"Não foi possível criar o cliente na {index}° posição, o nome não foi preenchido corretamente"
                : $"Não foi possível criar o cliente {name}, o nome passa do limite de 40 caracteres")
             select i).ToList();

        var selectedValidList = (from i in listInputCreateCustomer
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

    #region Validate Update
    public async Task<BaseResponse<List<CustomerValidate?>>> ValidateUpdateCustomer(List<CustomerValidate> listInputIdentityUpdateCustomer)
    {
        var response = new BaseResponse<List<CustomerValidate?>>();

        _ = (from i in listInputIdentityUpdateCustomer
             where i.ExistingCustomer == 0
             let name = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name
             let id = i.InputIdentityUpdateCustomer.Id
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível atualizar o cliente {name}, o seu Id {id} é inválido")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateCustomer
             where i.RepeatedId != 0
             let name = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name
             let id = i.InputIdentityUpdateCustomer.Id
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível atualizar o cliente {name}, o seu Id {id} foi digitado mais de uma vez na requisição")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateCustomer
             let cpf = i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF
             where cpf?.Length != 11
             let name = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível atualizar o cliente {name}, o cpf é inválido")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateCustomer
             let phone = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Phone
             where phone?.Length != 11
             let name = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível atualizar o cliente {name}, o telefone é inválido")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateCustomer
             where i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email?.Length > 60
             let name = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível atualizar o cliente {name}, o email passa do limite de 60 caracteres")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateCustomer
             where !i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email.Contains("@")
             let name = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível atualizar o cliente {name}, o email é inválido")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateCustomer
             where !i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email.EndsWith(".com")
             let name = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível atualizar o cliente {name}, o email é inválido")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateCustomer.Select((value, index) => new { Value = value, Index = index })
             let name = i.Value.InputIdentityUpdateCustomer.InputUpdateCustomer.Name
             where string.IsNullOrWhiteSpace(name) || name?.Length > 40
             let index = i.Index + 1
             let setInvalid = i.Value.SetInvalid()
             let message = response.AddErrorMessage(string.IsNullOrWhiteSpace(name)
                ? $"Não foi possível atualizar o cliente na {index}° posição, o nome não foi preenchido corretamente"
                : $"Não foi possível atualizar o cliente {name}, o nome passa do limite de 40 caracteres")
             select i).ToList();

        var selectedValidList = (from i in listInputIdentityUpdateCustomer
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

    #region Validate Delete
    public async Task<BaseResponse<List<CustomerValidate>>> ValidateDeleteCustomer(List<CustomerValidate> listInputIdentityDeleteCustomer)
    {
        var response = new BaseResponse<List<CustomerValidate>>();

        _ = (from i in listInputIdentityDeleteCustomer
             where i.RepeatedId != 0
             let id = i.InputIdentityDeleteCustomer.Id
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível deletar o cliente do Id: {id}, id foi digitado mais de uma vez na requisição")
             select i).ToList();

        _ = (from i in listInputIdentityDeleteCustomer
             where i.ExistingCustomer == 0
             let id = i.InputIdentityDeleteCustomer.Id
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível deletar o cliente do Id: {id}, id inválido")
             select i).ToList();

        var selectedValidList = (from i in listInputIdentityDeleteCustomer
                                 where !i.Invalid
                                 select i).ToList();

        if (!selectedValidList.Any())
        {
            response.Success = false;
            return response;
        }

        response.Content = selectedValidList;
        return response;

        #endregion
    }
}