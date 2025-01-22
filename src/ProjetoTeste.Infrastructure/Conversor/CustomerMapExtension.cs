using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Conversor
{
    public static class CustomerMapExtension
    {
        public static OutputCustomer? ToOutputCustomer(this Customer? customer)
        {
            return customer == null ? null : new OutputCustomer(
                customer.Id,
                customer.Name,
                customer.Email,
                customer.CPF,
                customer.Phone
            );
        }

        public static Customer? ToCustomer(this InputCreateCustomer input)
        {
            return input == null ? null : new Customer
                (
                    input.Name,
                    input.Email,
                    input.CPF,
                    input.Phone
                );
        }

        public static List<OutputCustomer>? ToListOutputCustomer(this List<Customer> customers)
        {
            return customers == null ? null : customers.Select(x => new OutputCustomer(
                x.Id,
                x.Name,
                x.Email,
                x.CPF,
                x.Phone
            )).ToList();
        }
    }
}