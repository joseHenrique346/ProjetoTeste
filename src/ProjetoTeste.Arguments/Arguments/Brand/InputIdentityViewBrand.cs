using ProjetoTeste.Arguments.Arguments.Base.Inputs;
using ProjetoTeste.Arguments.Arguments.Base.Inputs.Interfaces;

namespace ProjetoTeste.Arguments.Arguments.Brand
{
    public class InputIdentityViewBrand(long id) : BaseInputIdentityView<InputIdentityViewBrand>, IBaseIdentityView
    {
        public long Id { get; private set; } = id;
    }
}
