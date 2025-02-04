using ProjetoTeste.Arguments.Arguments.Base.Inputs;
using ProjetoTeste.Arguments.Arguments.Base.Inputs.Interfaces;

namespace ProjetoTeste.Arguments.Arguments.Product
{
    public class InputIdentityViewProduct(long id) : BaseInputIdentityView<InputIdentityViewProduct>, IBaseIdentityView
    {
        public long Id { get; private set; } = id;
    }
}
