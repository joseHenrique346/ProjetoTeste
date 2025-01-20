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

    public async Task<BaseResponse<List<OutputCustomer?>>> ValidateCreateCustomer(List<InputCreateCustomer> ListInputCreateCustomer)
    {
        var response = new BaseResponse<List<OutputCustomer?>>();

        if (ListInputCreateCustomer.Select(i => i.CPF).ToString().Length != 11)
            response.AddErrorMessage("Tamanho de CPF inválido, informe o CPF corretamente.");

        if (ListInputCreateCustomer.Select(i => i.Phone).ToString().Length != 11)
            response.AddErrorMessage("Tamanho de telefone inválido, informe o telefone corretamente.");

        if (ListInputCreateCustomer.Select(i => i.Email).ToString().Length > 60)
            response.AddErrorMessage("Email não pode ultrapassar 60 caracteres, informe corretamente.");

        if (ListInputCreateCustomer.Select(i => i.Name).ToString().Length > 40)
            response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres, informe corretamente.");

        if (response.Message.Count > 0)
            response.Success = false;

        return response;
    }

    #endregion

    #region Validate Update
    public async Task<BaseResponse<List<OutputCustomer?>>> ValidateUpdateCustomer(List<InputUpdateCustomer> inputUpdate)
    {
        var response = new BaseResponse<List<OutputCustomer?>>();

        if (inputUpdate.Select(i => i.CPF).ToString().Length != 11)
            response.AddErrorMessage("Tamanho de CPF inválido, informe o CPF corretamente.");

        if (inputUpdate.Select(i => i.Phone).ToString().Length != 11)
            response.AddErrorMessage("Tamanho de telefone inválido, informe o telefone corretamente.");

        if (inputUpdate.Select(i => i.Email).ToString().Length > 60)
            response.AddErrorMessage("Email não pode ultrapassar 60 caracteres, informe corretamente.");

        if (inputUpdate.Select(i => i.Name).ToString().Length > 40)
            response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres, informe corretamente.");

        if (response.Message.Count > 0)
            response.Success = false;

        return response;
    }
    #endregion

    #region Validate Delete
    public async Task<BaseResponse<List<bool>>> ValidateDeleteCustomer(List<long> id)
    {
        var response = new BaseResponse<List<bool>>();

        var existingCustomer = await _customerRepository.GetListByListId(id);
        if (existingCustomer == null)
            response.AddErrorMessage("Este Usuário não existe");

        if (response.Message.Count > 0)
            response.Success = false;
        else
            response.AddSuccessMessage($"O usuário {existingCustomer.Select(i => i.Name)} foi apagado com sucesso");

        return response;
    }
    #endregion
}
