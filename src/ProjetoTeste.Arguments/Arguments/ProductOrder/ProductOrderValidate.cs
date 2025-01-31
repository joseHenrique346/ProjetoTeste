using ProjetoTeste.Arguments.Arguments.Base;

namespace ProjetoTeste.Arguments.Arguments.ProductOrder
{
    public class ProductOrderValidate : BaseValidate
    {
        public InputCreateProductOrder? InputCreateProductOrder { get; private set; }
        public long ExistingOrder { get; private set; }
        public ProductDTO ExistingProduct { get; private set; }

        public ProductOrderValidate CreateProductOrder(InputCreateProductOrder inputCreateProductOrder, long existingOrder, ProductDTO existingProduct)
        {
            InputCreateProductOrder = inputCreateProductOrder;
            ExistingOrder = existingOrder;
            ExistingProduct = existingProduct;
            return this;
        }
    }
}
