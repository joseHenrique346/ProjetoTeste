using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Arguments.Arguments.Brand
{
    public class BrandValidate : BaseValidate
    {
        public InputCreateBrand InputCreateBrand { get; private set; }
        public string? RepeatedCode { get; private set; }
        public string? ExistingCodeId { get; private set; }

        public InputIdentityUpdateBrand InputIdentityUpdateBrand { get; private set; }
        public long CurrentBrand { get; private set; }
        public BrandDTO? ExistingCodeBrand { get; private set; }
        public long? RepeatedId { get; private set; }

        public InputIdentityDeleteBrand InputIdentityDeleteBrand { get; private set; }
        public long? ExistingBrand { get; private set; }
        public long? ExistingProductInBrand { get; private set; }

        public BrandValidate() { }

        public BrandValidate CreateValidate(InputCreateBrand inputCreateBrand, string repeatedCode, string existingCode)
        {
            InputCreateBrand = inputCreateBrand;
            RepeatedCode = repeatedCode;
            ExistingCodeId = existingCode;
            return this;
        }

        public BrandValidate UpdateValidate(InputIdentityUpdateBrand inputIdentityUpdateBrand, string repeatedCode, BrandDTO existingCode, long currentBrand, long repeatedId)
        {
            InputIdentityUpdateBrand = inputIdentityUpdateBrand;
            RepeatedCode = repeatedCode;
            RepeatedId = repeatedId;
            ExistingCodeBrand = existingCode;
            CurrentBrand = currentBrand;
            return this;
        }

        public BrandValidate DeleteValidate(InputIdentityDeleteBrand inputIdentityDeleteBrand, long existingBrand, long? existingProductInBrand, long repeatedId)
        {
            InputIdentityDeleteBrand = inputIdentityDeleteBrand;
            ExistingBrand = existingBrand;
            ExistingProductInBrand = existingProductInBrand;
            RepeatedId = repeatedId;
            return this;
        }
    }
}
