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

    public async Task<BaseResponse<InputCreateCustomer?>> ValidateCreateCustomer(InputCreateCustomer inputCreate)
    {
        var response = new BaseResponse<InputCreateCustomer?>();

        if (inputCreate.CPF.Length != 11)
            response.AddErrorMessage("Tamanho de CPF inválido, informe o CPF corretamente.");

        if (inputCreate.Phone.Length != 11)
            response.AddErrorMessage("Tamanho de telefone inválido, informe o telefone corretamente.");

        if (inputCreate.Email.Length > 60)
            response.AddErrorMessage("Email não pode ultrapassar 60 caracteres, informe corretamente.");

        if (inputCreate.Name.Length > 40)
            response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres, informe corretamente.");
        
        if (response.Message.Count > 0)
            response.Success = false;

        return response;
    }

    #endregion

    #region Validate Update
    public async Task<BaseResponse<InputUpdateCustomer?>> ValidateUpdateCustomer(InputUpdateCustomer inputUpdate)
    {
        var response = new BaseResponse<InputUpdateCustomer?>();

        if (inputUpdate.CPF.Length != 11)
            response.AddErrorMessage("Tamanho de CPF inválido, informe o CPF corretamente.");

        if (inputUpdate.Phone.Length != 11)
            response.AddErrorMessage("Tamanho de telefone inválido, informe o telefone corretamente.");

        if (inputUpdate.Email.Length > 60)
            response.AddErrorMessage("Email não pode ultrapassar 60 caracteres, informe corretamente.");

        if (inputUpdate.Name.Length > 40)
            response.AddErrorMessage("O nome não pode ultrapassar 40 caracteres, informe corretamente.");

        if (response.Message.Count > 0)
            response.Success = false;

        return response;
    }
    #endregion

    #region Validate Delete
    public async Task<BaseResponse<bool>> ValidateDeleteCustomer(long id)
    {
        var response = new BaseResponse<bool>();

        var existingCustomer = await _customerRepository.GetAsync(id);
        if (existingCustomer == null)
            response.AddErrorMessage("Este Usuário não existe");

        if (response.Message.Count > 0)
            response.Success = false;
        else
            response.AddSuccessMessage($"O usuário {existingCustomer.Name} foi apagado com sucesso");

        return response;
    }
    #endregion
}
