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
        #region Dependency Injection

        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerValidateService _customerValidateService;
        private readonly AppDbContext _context;

        public CustomerService(ICustomerRepository customerRepository, AppDbContext context, ICustomerValidateService customerValidateService)
        {
            _customerRepository = customerRepository;
            _context = context;
            _customerValidateService = customerValidateService;
        }

        #endregion

        #region Get
        public async Task<OutputCustomer> Get(long id)
        {
            var customer = await _customerRepository.GetAsync(id);
            return customer.ToOutputCustomer();
        }

        public async Task<List<OutputCustomer?>> GetAll()
        {
            var allCustomers = await _customerRepository.GetAllAsync();
            return allCustomers.ToList().ToListOutputProduct();
        }

        #endregion

        #region Create
        public async Task<BaseResponse<OutputCustomer>> Create(InputCreateCustomer inputCreate)
        {
            var result = await _customerValidateService.ValidateCreateCustomer(inputCreate);
            if (!result.Success)
            {
                return new BaseResponse<OutputCustomer> { Success = false, Message = result.Message };
            }

            var customer = new Customer
            {
                CPF = inputCreate.CPF,
                Email = inputCreate.Email,
                Name = inputCreate.Name,
                Phone = inputCreate.Phone
            };

            await _customerRepository.CreateAsync(customer);

            return new BaseResponse<OutputCustomer>
            {
                Success = true,
                Request = customer.ToOutputCustomer()
            };
        }
        #endregion

        #region Update
        public async Task<BaseResponse<OutputCustomer>> Update(InputUpdateCustomer inputUpdate)
        {
            var result = await _customerValidateService.ValidateUpdateCustomer(inputUpdate);
            var currentBrand = await _customerRepository.GetAsync(inputUpdate.Id);

            if (!result.Success)
            {
                return new BaseResponse<OutputCustomer> { Success = false, Message = result.Message };
            }

            currentBrand.Name = inputUpdate.Name;
            currentBrand.Phone = inputUpdate.Phone;
            currentBrand.Email = inputUpdate.Email;
            currentBrand.CPF = inputUpdate.CPF;

            _context.Customer.Update(currentBrand);

            return new BaseResponse<OutputCustomer>
            {
                Success = true,
                Request = currentBrand.ToOutputCustomer()
            };
        }
        #endregion

        #region Delete

        public async Task<BaseResponse<string>> Delete(long id)
        {
            var result =  await _customerValidateService.ValidateDeleteCustomer(id);
            if (!result.Success)
            {
                return new BaseResponse<string> { Success = false, Message = result.Message };
            }

            await _customerRepository.DeleteAsync(id);

            return new BaseResponse<string> { Success = true, Message = result.Message };
        }

        #endregion
    }
}