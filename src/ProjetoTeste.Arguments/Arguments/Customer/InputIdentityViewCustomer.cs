using ProjetoTeste.Arguments.Arguments.Base.Inputs;
using ProjetoTeste.Arguments.Arguments.Base.Inputs.Interfaces;

namespace ProjetoTeste.Arguments.Arguments.Customer
{
    public class InputIdentityViewCustomer(long id) : BaseInputIdentityView<InputIdentityViewCustomer>, IBaseIdentityView
    {
        public long Id { get; private set; } = id;
    }
}
