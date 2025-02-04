using ProjetoTeste.Arguments.Arguments.Base.Inputs;

namespace ProjetoTeste.Arguments.Arguments.Customer
{
    public class InputIdentityUpdateCustomer(long id, InputUpdateCustomer inputUpdateCustomer) : BaseInputIdentityUpdate<InputIdentityUpdateCustomer>
    {
        public long Id { get; private set; } = id;
        public InputUpdateCustomer InputUpdateCustomer { get; private set; } = inputUpdateCustomer;
    }
}