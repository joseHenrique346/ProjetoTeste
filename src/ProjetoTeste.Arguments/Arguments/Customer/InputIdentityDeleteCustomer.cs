using ProjetoTeste.Arguments.Arguments.Base.Inputs;

namespace ProjetoTeste.Arguments.Arguments.Customer
{
    public class InputIdentityDeleteCustomer(long id) : BaseInputIdentityDelete<InputIdentityDeleteCustomer>
    {
        public long Id { get; private set; } = id;
    }
}
