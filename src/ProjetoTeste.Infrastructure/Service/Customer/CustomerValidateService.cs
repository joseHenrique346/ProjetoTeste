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
             where i.InputCreateCustomer.CPF.Length != 11
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar o cliente {i.InputCreateCustomer.Name}, o cpf está inválido")
             select i).ToList();

        _ = (from i in listInputCreateCustomer
             where i.InputCreateCustomer.Phone.Length != 11
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar o cliente {i.InputCreateCustomer.Name}, o telefone está inválido")
             select i).ToList();

        _ = (from i in listInputCreateCustomer
             where string.IsNullOrEmpty(i.InputCreateCustomer.Email) || string.IsNullOrWhiteSpace(i.InputCreateCustomer.Email)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar o cliente {i.InputCreateCustomer.Name}, o email não pode ser vazio")
             select i).ToList();

        _ = (from i in listInputCreateCustomer
             where i.InputCreateCustomer.Email.Length > 60 ||
             !i.InputCreateCustomer.Email.Contains("@")
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputCreateCustomer.Email.Length > 60 ? $"Não foi possível criar o cliente {i.InputCreateCustomer.Name}, o email passa do limite de 60 caracteres" : $"Não foi possível criar o cliente {i.InputCreateCustomer.Name}, o email está inválido")
             select i).ToList();

        _ = (from i in listInputCreateCustomer
             where !i.InputCreateCustomer.Email.EndsWith(".com")
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar o cliente {i.InputCreateCustomer.Name}, o email é inválido")
             select i).ToList();

        _ = (from i in listInputCreateCustomer
             where i.InputCreateCustomer.Name.Length > 40 || string.IsNullOrEmpty(i.InputCreateCustomer.Name) || string.IsNullOrWhiteSpace(i.InputCreateCustomer.Name)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputCreateCustomer.Name.Length > 40 ? $"Não foi possível criar o cliente {i.InputCreateCustomer.Name}, o nome passa do limite de 40 caracteres" : $"Não foi possível criar o cliente {i.InputCreateCustomer.Name}, o nome está inválido")
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
             where i.RepeatedCode != 0
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar o cliente {i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name}, o seu Id {i.InputIdentityUpdateCustomer.Id} foi digitado mais de uma vez na requisição")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateCustomer
             where i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF.Length != 11 || i.InputIdentityUpdateCustomer.InputUpdateCustomer.Phone.Length != 11
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputCreateCustomer.CPF.Length != 11 ? $"Não foi possível criar o cliente {i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name}, o cpf é inválido" : $"Não foi possível criar o cliente {i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name}, o telefone é inválido")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateCustomer
             where string.IsNullOrEmpty(i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email) || string.IsNullOrWhiteSpace(i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar o cliente {i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name}, o email não pode ser vazio")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateCustomer
             where i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email.Length > 60 || !i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email.Contains("@")
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email.Length > 60 ? $"Não foi possível criar o cliente {i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name}, o email passa do limite de 60 caracteres" : $"Não foi possível criar o cliente {i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name}, o email é inválido")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateCustomer
             where !i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email.EndsWith(".com")
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível criar o cliente {i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name}, o email é inválido")
             select i).ToList();

        _ = (from i in listInputIdentityUpdateCustomer
             where i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name.Length > 40 || string.IsNullOrEmpty(i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name) || string.IsNullOrWhiteSpace(i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name)
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage(i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name.Length > 40 ? $"Não foi possível criar o cliente {i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name}, o nome passa do limite de 40 caracteres" : $"Não foi possível criar o cliente {i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name}, o nome é inválido")
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
             where i.ExistingCustomer == 0
             let setInvalid = i.SetInvalid()
             let message = response.AddErrorMessage($"Não foi possível deletar o cliente do Id: {i.InputIdentityDeleteCustomer.Id}, id inválido")
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
    }
}

#endregion