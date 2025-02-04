using ProjetoTeste.Arguments.Arguments.Base.Inputs;
using ProjetoTeste.Arguments.Arguments.Base.Inputs.Interfaces;

namespace ProjetoTeste.Arguments.Arguments.Order
{
    public class InputIdentityViewOrder(long id) : BaseInputIdentityView<InputIdentityViewOrder>, IBaseIdentityView
    {
        public long Id { get; private set; } = id;
    }
}