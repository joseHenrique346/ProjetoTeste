using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Service
{
    public class CustomerService
    {
        protected readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Response<string?>> ValidateGetCustomerAsync(long id)
        {
            var existingId = await _customerRepository.GetAsync(id);
            if (existingId is null)
            {
                return new Response<string?>
                {
                    Success = false,
                    Message = "*ERRO* Tem certeza que digitou o ID certo?"
                };
            }

            return new Response<string?>
            {
                Success = true
            };
        }

        public async Task<Response<Customer?>> Get(long id)
        {
            var validationResponse = await ValidateGetCustomerAsync(id);

            if (!validationResponse.Success)
            {
                return new Response<Customer?>
                {
                    Success = false,
                    Message = validationResponse.Message
                };
            }

            var customer = await _customerRepository.GetAsync(id);

            return new Response<Customer?>
            {
                Success = true,
                Request = customer
            };
        }

        public async Task<Response<string?>> ValidateCreateCustomerAsync(InputCreateCustomer input)
        {
            if (input.CPF.Length < 11 || input.CPF.Length > 11)
            {
                return new Response<string?>
                {
                    Success = false,
                    Message = "Tamanho de CPF inválido, informe o CPF corretamente."
                };
            }

            if (input.Phone < 11 || input.Phone > 11)
            {
                return new Response<string?>
                {
                    Success = false,
                    Message = "Tamanho de telefone inválido, informe o telefone corretamente."
                };
            }

            if (input.Email.Length > 60)
            {
                return new Response<string?>
                {
                    Success = false,
                    Message = "Email não pode ultrapassar 60 caracteres, informe corretamente."
                };
            }

            if (input.Name.Length > 40)
            {
                return new Response<string?>
                {
                    Success = false,
                    Message = "O nome não pode ultrapassar 40 caracteres, informe corretamente."
                };
            }

            return new Response<string?>
            {
                Success = true,
            };
        }

        public async Task<Response<Customer>> Create(InputCreateCustomer input)
        {
            var result = await ValidateCreateCustomerAsync(input);
            if (!result.Success)
            {
                return new Response<Customer> { Success = false, Message = result.Message };
            }

            var customer = new Customer { CPF = input.CPF, Email = input.Email, Name = input.Name, Phone = input.Phone };
            return new Response<Customer> { Success = true, Request = customer };
        }
    }
}