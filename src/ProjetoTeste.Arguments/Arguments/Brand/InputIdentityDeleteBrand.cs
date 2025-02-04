using ProjetoTeste.Arguments.Arguments.Base.Inputs;

namespace ProjetoTeste.Arguments.Arguments.Brand
{
    public class InputIdentityDeleteBrand(long id) : BaseInputIdentityDelete<InputIdentityDeleteBrand>
    {
        public long Id { get; private set; } = id;
    }
}
