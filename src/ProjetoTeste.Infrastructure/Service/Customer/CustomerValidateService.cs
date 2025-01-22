using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;

namespace ProjetoTeste.Infrastructure.Service;

public class CustomerValidateService : ICustomerValidateService
{
    #region Dependency Injection
    public readonly ICustomerRepository _customerRepository;

    public CustomerValidateService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    #endregion

    #region Validate Create

    public async Task<BaseResponse<List<OutputCustomer?>>> ValidateCreateCustomer(List<InputCreateCustomer> listInputCreateCustomer)
    {
        var response = new BaseResponse<List<OutputCustomer?>>();

        for (var i = 0; i < listInputCreateCustomer.Count; i++)
        {
            if (listInputCreateCustomer[i] is null)
                response.AddErrorMessage("Id não encontrado");

            if (listInputCreateCustomer[i].CPF.Length != 11)
                response.AddErrorMessage("Tamanho de CPF inválido, informe o CPF corretamente.");

            if (listInputCreateCustomer[i].Phone.Length != 11)
                response.AddErrorMessage("Tamanho de telefone inválido, informe o telefone corretamente.");

            if (listInputCreateCustomer[i].Email.Length > 60)
                response.AddErrorMessage("Email não pode ultrapassar 60 caracteres, informe corretamente.");

            if (!listInputCreateCustomer[i].Email.Contains("@") && !listInputCreateCustomer[i].Email.EndsWith(".com") || !listInputCreateCustomer[i].Email.EndsWith(".com.br"))
                response.AddErrorMessage("Email inválido, digite corretamente");

            if (listInputCreateCustomer[i].Name.Length > 40)
                response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres, informe corretamente.");

            if (string.IsNullOrEmpty(listInputCreateCustomer[i].Name))
                response.AddErrorMessage("Nome não pode ser vazio");

            if (response.Message.Count > 0)
            {
                response.Success = false;
                listInputCreateCustomer.Remove(listInputCreateCustomer[i]);
            }
        }
        return response;
    }

    #endregion

    #region Validate Update
    public async Task<BaseResponse<List<OutputCustomer?>>> ValidateUpdateCustomer(List<InputIdentityUpdateCustomer> listInputIdentityUpdateCustomer)
    {
        var response = new BaseResponse<List<OutputCustomer?>>();

        for (var i = 0; i < listInputIdentityUpdateCustomer.Count; i++)
        {
            if (listInputIdentityUpdateCustomer[i] is null)
                response.AddErrorMessage("Id não econtrado.");

            if (listInputIdentityUpdateCustomer[i].InputUpdateCustomer.CPF.Length != 11)
                response.AddErrorMessage("Tamanho de CPF inválido, informe o CPF corretamente.");

            if (listInputIdentityUpdateCustomer[i].InputUpdateCustomer.Phone.Length != 11)
                response.AddErrorMessage("Tamanho de telefone inválido, informe o telefone corretamente.");

            if (listInputIdentityUpdateCustomer[i].InputUpdateCustomer.Email.Length > 60)
                response.AddErrorMessage("Email não pode ultrapassar 60 caracteres, informe corretamente.");

            if (listInputIdentityUpdateCustomer[i].InputUpdateCustomer.Name.Length > 40)
                response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres, informe corretamente.");

            if (response.Message.Count > 0)
            {
                response.Success = false;
                listInputIdentityUpdateCustomer.Remove(listInputIdentityUpdateCustomer[i]);
            }
        }
        return response;
    }
    #endregion

    #region Validate Delete
    public async Task<BaseResponse<List<bool>>> ValidateDeleteCustomer(List<InputIdentityDeleteCustomer> listInputIdentityDeleteCustomer)
    {
        var response = new BaseResponse<List<bool>>();

        var existingCustomer = await _customerRepository.GetListByListIdWhere(listInputIdentityDeleteCustomer.Select(i => i.Id).ToList());
        for (var i = 0; i < listInputIdentityDeleteCustomer.Count; i++)
        {

            if (existingCustomer[i] == null)
                response.AddErrorMessage("Este Usuário não existe");

            if (response.Message.Count > 0)
                response.Success = false;
            else
                response.AddSuccessMessage($"O usuário {existingCustomer.Select(i => i.Name)} foi apagado com sucesso");
        }

        return response;
    }
}

    #endregion