using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Service
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository customerRepository, AppDbContext context, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<string?>> ValidateGetCustomerAsync(long id)
        {
            await _unitOfWork.BeginTransactionAsync();
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

        public async Task<Response<OutputCustomer>> Get(long id)
        {
            var validationResponse = await ValidateGetCustomerAsync(id);

            if (!validationResponse.Success)
            {
                return new Response<OutputCustomer>
                {
                    Success = false,
                    Message = validationResponse.Message
                };
            }

            var customer = await _customerRepository.GetAsync(id);

            return new Response<OutputCustomer>
            {
                Success = true,
                Request = customer.ToOutputCustomer()
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

            if (input.Phone.Length != 11)
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

        public async Task<Response<OutputCustomer>> Create(InputCreateCustomer input)
        {
            var result = await ValidateCreateCustomerAsync(input);
            await _unitOfWork.BeginTransactionAsync();
            if (!result.Success)
            {
                return new Response<OutputCustomer> { Success = false, Message = result.Message };
            }

            var customer = new Customer
            {
                CPF = input.CPF,
                Email = input.Email,
                Name = input.Name,
                Phone = input.Phone
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new Response<OutputCustomer>
            {
                Success = true,
                Request = customer.ToOutputCustomer()
            };
        }
    }
}