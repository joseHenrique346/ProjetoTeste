using ProjetoTeste.Arguments.Arguments;
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

        public static ProductDTO? ToProductDto(this Product product)
        {
            return product == null ? null : new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code,
                Description = product.Description,
                Price = product.Price,
                BrandId = product.BrandId,
                Stock = product.Stock
            };
        }

        public static Product? ToProduct(this ProductDTO productDTO)
        {
            return productDTO == null ? null : new Product
            {
                Name = productDTO.Name,
                Code = productDTO.Code,
                Description = productDTO.Description,
                Price = productDTO.Price,
                BrandId = productDTO.BrandId,
                Stock = productDTO.Stock
            };
        }

        public static List<Product>? ToListProduct(this List<ProductDTO> listProductDto)
        {
            return listProductDto == null ? null : listProductDto.Select(x => new Product(
                x.Name,
                x.Description,
                x.Code,
                x.BrandId,
                x.Price,
                x.Stock
            )).ToList();
        }

        //public static List<Product>? ToListProduct(this List<InputIdentityUpdateProduct> listInputIdentityUpdateProduct)
        //{
        //    return listInputIdentityUpdateProduct == null ? null : listInputIdentityUpdateProduct.Select(x => new Product(
        //        x.InputUpdateProduct.Name,
        //        x.InputUpdateProduct.Description,
        //        x.InputUpdateProduct.Code,
        //        x.InputUpdateProduct.BrandId,
        //        x.InputUpdateProduct.Price,
        //        x.InputUpdateProduct.Stock
        //    )).ToList();
        //}

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