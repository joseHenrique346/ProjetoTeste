using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Conversor
{
    public static class ProductMapExtension
    {
        public static OutputProduct? ToOutputProduct(this Product product)
        {
            return product == null ? null : new OutputProduct(
                product.Id,
                product.Name,
                product.Description,
                product.Code,
                product.Price,
                product.BrandId,
                product.Stock
            );
        }

        public static Product? ToProduct(this InputCreateProduct input)
        {
            return input == null ? null : new Product
            {
                Name = input.Name,
                Code = input.Code,
                Description = input.Description,
                Price = input.Price,
                BrandId = input.BrandId,
                Stock = input.Stock
            };
        }

        public static List<OutputProduct>? ToListOutputProduct(this List<Product> products)
        {
            return products == null ? null : products.Select(x => new OutputProduct(
                x.Id,
                x.Name,
                x.Description,
                x.Code,
                x.Price,
                x.BrandId,
                x.Stock
            )).ToList();
        }
    }
}