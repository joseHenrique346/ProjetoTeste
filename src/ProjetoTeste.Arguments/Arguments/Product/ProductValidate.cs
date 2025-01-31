using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Arguments.Arguments.Product
{
    public class ProductValidate : BaseValidate
    {
        public InputCreateProduct InputCreateProduct { get; set; }
        public string ExistingCodeId { get; private set; }
        public string RepeatedCode { get; private set; }
        public long ExistingBrand { get; private set; }

        public InputIdentityUpdateProduct InputIdentityUpdateProduct { get; set; }
        public long CurrentProduct { get; private set; }
        public ProductDTO? ExistingCodeProduct { get; private set; }

        public InputIdentityDeleteProduct InputIdentityDeleteProduct { get; set; }
        public ProductDTO Product { get; private set; }
        public long RepeatedId { get; private set; }

        public ProductValidate CreateValidate(InputCreateProduct inputCreateProduct, string existingCodeId, long existingBrand, string repeatedCode)
        {
            InputCreateProduct = inputCreateProduct;
            ExistingCodeId = existingCodeId;
            ExistingBrand = existingBrand;
            RepeatedCode = repeatedCode;
            return this;
        }

        public ProductValidate UpdateValidate(InputIdentityUpdateProduct inputIdentityUpdateProduct, ProductDTO? existingCodeProduct, long currentProduct, long existingBrand)
        {
            InputIdentityUpdateProduct = inputIdentityUpdateProduct;
            ExistingCodeProduct = existingCodeProduct;
            CurrentProduct = currentProduct;
            ExistingBrand = existingBrand;
            return this;
        }

        public ProductValidate DeleteValidate(InputIdentityDeleteProduct inputIdentityDeleteProduct, ProductDTO product, long repeatedId)
        {
            InputIdentityDeleteProduct = inputIdentityDeleteProduct;
            Product = product;
            RepeatedId = repeatedId;
            return this;
        }
    }
}
