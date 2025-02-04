using ProjetoTeste.Arguments.Arguments.Base.Inputs;

namespace ProjetoTeste.Arguments.Arguments.Product
{
    public class InputIdentityUpdateProduct(long id, InputUpdateProduct inputUpdateProduct) : BaseInputIdentityUpdate<InputIdentityUpdateProduct>

    {
        public long Id { get; private set; } = id;
        public InputUpdateProduct InputUpdateProduct { get; private set; } = inputUpdateProduct;
    }
}
