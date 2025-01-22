using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Service
{
    public class CustomerService : ICustomerService
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
        public async Task<List<OutputCustomer>> Get(List<InputIdentityViewCustomer> listInputIdentityViewCustomer)
        {
            var customer = await _customerRepository.GetListByListIdWhere(listInputIdentityViewCustomer.Select(i => i.Id).ToList());
            return customer.ToListOutputCustomer();
        }

        public async Task<List<OutputCustomer?>> GetAll()
        {
            var allCustomers = await _customerRepository.GetAllAsync();
            return allCustomers.ToListOutputCustomer();
        }

        #endregion

        #region Create
        public async Task<BaseResponse<List<OutputCustomer>>> Create(List<InputCreateCustomer> inputCreateCustomer)
        {
            var result = await _customerValidateService.ValidateCreateCustomer(inputCreateCustomer);
            if (!result.Success)
            {
                return new BaseResponse<List<OutputCustomer>> { Success = false, Message = result.Message };
            }

            var listCustomer = (from i in inputCreateCustomer
                                select new Customer(i.Name, i.Email, i.CPF, i.Phone)).ToList();

            await _customerRepository.CreateAsync(listCustomer);

            return new BaseResponse<List<OutputCustomer>>
            {
                Success = true,
                Content = listCustomer.ToListOutputCustomer()
            };
        }
        #endregion

        #region Update
        public async Task<BaseResponse<List<OutputCustomer>>> Update(List<InputIdentityUpdateCustomer> listInputIdentityUpdateCustomer)
        {
            var result = await _customerValidateService.ValidateUpdateCustomer(listInputIdentityUpdateCustomer);
            var currentCustomer = await _customerRepository.GetListByListIdWhere(listInputIdentityUpdateCustomer.Select(i => i.Id).ToList());

            if (!result.Success)
            {
                return new BaseResponse<List<OutputCustomer>> { Success = false, Message = result.Message };
            }

            for (var i = 0; i < listInputIdentityUpdateCustomer.Count; i++)
            {
                currentCustomer[i].Name = listInputIdentityUpdateCustomer[i].InputUpdateCustomer.Name;
                currentCustomer[i].Email = listInputIdentityUpdateCustomer[i].InputUpdateCustomer.Email;
                currentCustomer[i].CPF = listInputIdentityUpdateCustomer[i].InputUpdateCustomer.CPF;
                currentCustomer[i].Phone = listInputIdentityUpdateCustomer[i].InputUpdateCustomer.Phone;
            }

            foreach (var i in currentCustomer)
            {
                _context.Customer.Update(i);
            }

            return new BaseResponse<List<OutputCustomer>>
            {
                Success = true,
                Content = currentCustomer.ToListOutputCustomer()
            };
        }
        #endregion

        #region Delete

        public async Task<BaseResponse<string>> Delete(List<InputIdentityDeleteCustomer> listInputIdentityDeleteCustomers)
        {
            var result = await _customerValidateService.ValidateDeleteCustomer(listInputIdentityDeleteCustomers);
            if (!result.Success)
            {
                return new BaseResponse<string> { Success = false, Message = result.Message };
            }

            var customerDeleted = await _customerRepository.GetListByListIdWhere(listInputIdentityDeleteCustomers.Select(i => i.Id).ToList());

            await _customerRepository.DeleteAsync(customerDeleted);

            return new BaseResponse<string> { Success = true, Message = result.Message };
        }

        #endregion
    }
}