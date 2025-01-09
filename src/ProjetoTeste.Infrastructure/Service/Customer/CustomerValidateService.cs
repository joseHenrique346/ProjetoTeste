using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Service;

namespace ProjetoTeste.Infrastructure.Service;

public class CustomerValidateService : ICustomerValidateService
{
    public async Task<Response<InputCreateCustomer?>> ValidateCreateCustomer(InputCreateCustomer input)
    {
        if (input.CPF.Length < 11 || input.CPF.Length > 11)
        {
            return new Response<InputCreateCustomer?> { Success = false, Message = "Tamanho de CPF inválido, informe o CPF corretamente." };
        }

        if (input.Phone.Length != 11)
        {
            return new Response<InputCreateCustomer?> { Success = false, Message = "Tamanho de telefone inválido, informe o telefone corretamente." };
        }

        if (input.Email.Length > 60)
        {
            return new Response<InputCreateCustomer?> { Success = false, Message = "Email não pode ultrapassar 60 caracteres, informe corretamente." };
        }

        if (input.Name.Length > 40)
        {
            return new Response<InputCreateCustomer?> { Success = false, Message = "O nome não pode ultrapassar 40 caracteres, informe corretamente." };
        }

        return new Response<InputCreateCustomer?> { Success = true, Request = input };
    }
}