using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Response;
using ProjetoTeste.Infrastructure.Conversor;
using ProjetoTeste.Infrastructure.Interface.Repository;
using ProjetoTeste.Infrastructure.Interface.Service;
using ProjetoTeste.Infrastructure.Persistence.Context;
using ProjetoTeste.Infrastructure.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

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
        public async Task<BaseResponse<List<OutputCustomer>>> Create(List<InputCreateCustomer> listInputCreateCustomer)
        {
            var response = new BaseResponse<List<OutputCustomer>>();

            var listInputValidateCreate = listInputCreateCustomer.Select(i => new CustomerValidate().CreateValidate(i)).ToList();

            var result = await _customerValidateService.ValidateCreateCustomer(listInputValidateCreate);
            response.Success = result.Success;
            response.Message = result.Message;

            if (!response.Success)
                return response;

            var listCustomer = (from i in result.Content
                                select new Customer(i.InputCreateCustomer.Name, i.InputCreateCustomer.Email, i.InputCreateCustomer.CPF, i.InputCreateCustomer.Phone)).ToList();

            await _customerRepository.CreateAsync(listCustomer);

            response.Content = listCustomer.ToListOutputCustomer();

            return response;
        }
        #endregion

        #region Update
        public async Task<BaseResponse<List<OutputCustomer>>> Update(List<InputIdentityUpdateCustomer> listInputIdentityUpdateCustomer)
        {
            var response = new BaseResponse<List<OutputCustomer>>();
            var listRepeatedCode = (from i in listInputIdentityUpdateCustomer
                                    where listInputIdentityUpdateCustomer.Count(j => j.Id == i.Id) > 1
                                    select i.Id).ToList();

            var listInputUpdate = (from i in listInputIdentityUpdateCustomer
                                   select new
                                   {
                                       InputUpdate = i,
                                       RepeatedCode = listRepeatedCode.FirstOrDefault(j => i.Id == j)
                                   }).ToList();

            var listInputValidateUpdate = listInputUpdate.Select(i => new CustomerValidate().UpdateValidate(i.InputUpdate, i.RepeatedCode)).ToList();

            var result = await _customerValidateService.ValidateUpdateCustomer(listInputValidateUpdate);
            var currentCustomer = await _customerRepository.GetListByListIdWhere(listInputIdentityUpdateCustomer.Select(i => i.Id).ToList());

            response.Success = result.Success;
            response.Message = result.Message;

            if (!result.Success)
                return response;

            var updatedList = (from i in result.Content
                               from j in currentCustomer
                               let name = j.Name = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Name
                               let cpf = j.CPF = i.InputIdentityUpdateCustomer.InputUpdateCustomer.CPF
                               let email = j.Email = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Email
                               let phone = j.Phone = i.InputIdentityUpdateCustomer.InputUpdateCustomer.Phone
                               select j).ToList();

            await _customerRepository.Update(updatedList);

            response.Content = updatedList.ToListOutputCustomer();
            return response;
        }

        #endregion

        #region Delete

        public async Task<BaseResponse<bool>> Delete(List<InputIdentityDeleteCustomer> listInputIdentityDeleteCustomers)
        {
            var response = new BaseResponse<bool>();
            var existingCustomer = await _customerRepository.GetListByListIdWhere(listInputIdentityDeleteCustomers.Select(i => i.Id).ToList());
            var selectedExistingCustomer = existingCustomer.Select(i => i.Id);

            var listInputDelete = (from i in listInputIdentityDeleteCustomers
                                   select new
                                   {
                                       InputDelete = i,
                                       ExistingCustomer = selectedExistingCustomer.FirstOrDefault(j => i.Id == j)
                                   }).ToList();

            var listInputValidateDelete = (listInputDelete.Select(i => new CustomerValidate().DeleteValidate(i.InputDelete, i.ExistingCustomer))).ToList();

            var result = await _customerValidateService.ValidateDeleteCustomer(listInputValidateDelete);

            response.Success = result.Success;
            response.Message = result.Message;

            if (!response.Success)
                return response;

            var deletedCustomer = (from i in result.Content
                                   from j in existingCustomer
                                   where j.Id == i.ExistingCustomer
                                   let message = response.AddSuccessMessage($"A marca {j.Name}, Id: {j.Id} foi deletada com sucesso!")
                                   select j).ToList();

            await _customerRepository.DeleteAsync(deletedCustomer);

            response.Content = true;

            return response;
        }
    }
}

#endregion