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
        if (inputCreate.CPF.Length != 11)
        {
            return new BaseResponse<InputCreateCustomer?> { Success = false, Message = "Tamanho de CPF inválido, informe o CPF corretamente." };
        }

        if (inputCreate.Phone.Length != 11)
        {
            return new BaseResponse<InputCreateCustomer?> { Success = false, Message = "Tamanho de telefone inválido, informe o telefone corretamente." };
        }

        if (inputCreate.Email.Length > 60)
        {
            return new BaseResponse<InputCreateCustomer?> { Success = false, Message = "Email não pode ultrapassar 60 caracteres, informe corretamente." };
        }

        if (inputCreate.Name.Length > 40)
        {
            return new BaseResponse<InputCreateCustomer?> { Success = false, Message = "O nome não pode ultrapassar 40 caracteres, informe corretamente." };
        }

        return new BaseResponse<InputCreateCustomer?> { Success = true, Request = inputCreate };
    }

    #endregion

    #region Validate Update
    public async Task<BaseResponse<InputUpdateCustomer?>> ValidateUpdateCustomer(InputUpdateCustomer inputUpdate)
    {
        if (inputUpdate.CPF.Length != 11)
        {
            return new BaseResponse<InputUpdateCustomer?> { Success = false, Message = "Tamanho de CPF inválido, informe o CPF corretamente." };
        }

        if (inputUpdate.Phone.Length != 11)
        {
            return new BaseResponse<InputUpdateCustomer?> { Success = false, Message = "Tamanho de telefone inválido, informe o telefone corretamente." };
        }

        if (inputUpdate.Email.Length > 60)
        {
            return new BaseResponse<InputUpdateCustomer?> { Success = false, Message = "Email não pode ultrapassar 60 caracteres, informe corretamente." };
        }

        if (inputUpdate.Name.Length > 40)
        {
            return new BaseResponse<InputUpdateCustomer?> { Success = false, Message = "O nome não pode ultrapassar 40 caracteres, informe corretamente." };
        }

        return new BaseResponse<InputUpdateCustomer?> { Success = true, Request = inputUpdate };
    }
    #endregion

    #region Validate Delete
    public async Task<BaseResponse<bool>> ValidateDeleteCustomer(long id)
    {
        var existingCustomer = await _customerRepository.GetAsync(id);
        if (existingCustomer == null)
        {
            return new BaseResponse<bool> { Success = false, Message = "Este Usuário não existe" };
        }

        return new BaseResponse<bool> { Success = true, Request = true, Message = $"O usuário {existingCustomer.Name} foi apagado com sucesso" };
    }
    #endregion
}
