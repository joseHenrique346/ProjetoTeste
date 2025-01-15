using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Service
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerValidateService _customerValidateService;
        private readonly AppDbContext _context;

        public CustomerService(ICustomerRepository customerRepository, AppDbContext context, ICustomerValidateService customerValidateService)
        {
            _customerRepository = customerRepository;
            _context = context;
            _customerValidateService = customerValidateService;
        }

        public async Task<Response<OutputCustomer>> Get(long id)
        {
            var customer = await _customerRepository.GetAsync(id);

            return new Response<OutputCustomer>
            {
                Success = true,
                Request = customer.ToOutputCustomer()
            };
        }

        public async Task<Response<OutputCustomer>> Create(InputCreateCustomer input)
        {
            var result = await _customerValidateService.ValidateCreateCustomer(input);
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

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return new Response<OutputCustomer>
            {
                Success = true,
                Request = customer.ToOutputCustomer()
            };
        }
    }
}