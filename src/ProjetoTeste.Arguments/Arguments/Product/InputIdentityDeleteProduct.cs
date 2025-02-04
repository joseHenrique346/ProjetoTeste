using ProjetoTeste.Arguments.Arguments.Base.Inputs;

namespace ProjetoTeste.Arguments.Arguments.Product
{
    public class InputIdentityDeleteProduct(long id) : BaseInputIdentityDelete<InputIdentityDeleteProduct>
    {
        public long Id { get; private set; } = id;
    }
}
