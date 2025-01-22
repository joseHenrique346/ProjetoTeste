using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Arguments.Arguments.Brand
{
    public class BrandValidate : BaseValidate
    {
        public InputCreateBrand InputCreateBrand { get; private set; }
        public InputIdentityUpdateBrand InputIdentityUpdateBrand { get; private set; }

        public BrandValidate UpdateValidate(InputIdentityUpdateBrand inputIdentityUpdateBrand)
        {
            InputIdentityUpdateBrand = inputIdentityUpdateBrand;
            return this;
        }

        public BrandValidate CreateValidate(InputCreateBrand inputCreateBrand)
        {
            InputCreateBrand = inputCreateBrand;
            return this;
        }
    }
}
