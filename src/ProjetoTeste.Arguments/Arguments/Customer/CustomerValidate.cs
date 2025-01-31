using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Arguments.Arguments.Customer
{
    public class CustomerValidate : BaseValidate
    {
        public InputCreateCustomer InputCreateCustomer { get; set; }
        public InputIdentityUpdateCustomer InputIdentityUpdateCustomer { get; set; }
        public long RepeatedId { get; private set; }
        public long ExistingCustomer { get; private set; }
        public InputIdentityDeleteCustomer InputIdentityDeleteCustomer { get; set; }

        public CustomerValidate CreateValidate(InputCreateCustomer inputCreateCustomer)
        {
            InputCreateCustomer = inputCreateCustomer;
            return this;
        }

        public CustomerValidate UpdateValidate(InputIdentityUpdateCustomer inputIdentityUpdateCustomer, long repeatedCode, long existingCustomer)
        {
            InputIdentityUpdateCustomer = inputIdentityUpdateCustomer;
            RepeatedId = repeatedCode;
            return this;
        }

        public CustomerValidate DeleteValidate(InputIdentityDeleteCustomer inputIdentityDeleteCustomer, long existingCustomer, long repeatedId)
        {
            InputIdentityDeleteCustomer = inputIdentityDeleteCustomer;
            ExistingCustomer = existingCustomer;
            RepeatedId = repeatedId;
            return this;
        }
    }
}
