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
            var customer = await _customerRepository.GetListByListId(id);
            return customer.ToOutputCustomer();
        }

        public async Task<List<OutputCustomer?>> GetAll()
        {
            var allCustomers = await _customerRepository.GetAllAsync();
            return allCustomers.ToList().ToListOutputProduct();
        }

        #endregion

        #region Create
        public async Task<BaseResponse<List<OutputCustomer>>> Create(List<InputCreateCustomer> inputCreate)
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

            foreach (var i in customer)
            {
                await _customerRepository.CreateAsync(i);
            }

            return new BaseResponse<OutputCustomer>
            {
                Success = true,
                Content = customer.ToOutputCustomer()
            };
        }
        #endregion

        #region Update
        public async Task<BaseResponse<List<OutputCustomer>>> Update(List<InputUpdateCustomer> inputUpdate)
        {
            var result = await _customerValidateService.ValidateUpdateCustomer(inputUpdate);
            var currentCustomer = await _customerRepository.GetListByListId(inputUpdate.Select(i => i.Id).ToList());

            if (!result.Success)
            {
                return new BaseResponse<List<OutputCustomer>> { Success = false, Message = result.Message };
            }

            foreach (var i in currentCustomer)
            {
                i.Name = inputUpdate.Select(i => i.Name).ToString();
                i.Email = inputUpdate.Select(i => i.Email).ToString();
                i.CPF = inputUpdate.Select(i => i.CPF).ToString();
                i.Phone = inputUpdate.Select(i => i.Phone).ToString();
            }

            foreach (var i in currentCustomer)
            {
                _context.Customer.Update(i);
            }

            return new BaseResponse<List<OutputCustomer>>
            {
                Success = true,
                Content = currentCustomer.ToListOutputProduct()
            };
        }
        #endregion

        #region Delete

        public async Task<BaseResponse<string>> Delete(long id)
        {
            var result = await _customerValidateService.ValidateDeleteCustomer(id);
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