using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Arguments.Arguments.Brand
{
    public class BrandValidate : BaseValidate
    {
        public InputCreateBrand InputCreateBrand { get; private set; }
        public string? RepeatedCode { get; private set; }
        public string? ExistingCode { get; private set; }

        public InputIdentityUpdateBrand InputIdentityUpdateBrand { get; private set; }
        public long CurrentBrand { get; private set; }
        public long RepeatedId { get; private set; }

        public InputIdentityDeleteBrand InputIdentityDeleteBrand { get; private set; }
        public long ExistingBrand { get; private set; }
        public long? ExistingProductInBrand { get; private set; }

        public BrandValidate() { }
        
        public BrandValidate CreateValidate(InputCreateBrand inputCreateBrand, string repeatedCode, string existingCode)
        {
            InputCreateBrand = inputCreateBrand;
            RepeatedCode = repeatedCode;
            ExistingCode = existingCode;
            return this;
        }

        public BrandValidate UpdateValidate(InputIdentityUpdateBrand inputIdentityUpdateBrand, string repeatedCode, string existingCode, long currentBrand, long repeatedId)
        {
            InputIdentityUpdateBrand = inputIdentityUpdateBrand;
            RepeatedCode = repeatedCode;
            RepeatedId = repeatedId;
            ExistingCode = existingCode;
            CurrentBrand = currentBrand;
            return this;
        }

        public BrandValidate DeleteValidate(InputIdentityDeleteBrand inputIdentityDeleteBrand, long existingBrand, long? existingProductInBrand)
        {
            InputIdentityDeleteBrand = inputIdentityDeleteBrand;
            ExistingBrand = existingBrand;
            ExistingProductInBrand = existingProductInBrand;
            return this;
        }
    }
}
