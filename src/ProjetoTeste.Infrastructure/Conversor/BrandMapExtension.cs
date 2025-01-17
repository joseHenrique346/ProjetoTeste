using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Conversor
{
    public static class BrandMapExtension
    {
        public static OutputBrand? ToOutputBrand(this Brand brand)
        {
            return brand == null ? null : new OutputBrand(
                brand.Id,
                brand.Name,
                brand.Code,
                brand.Description
            );
        }

        public static Brand ToBrand(this InputCreateBrand input)
        {
            return new Brand
            {
                Name = input.Name,
                Code = input.Code,
                Description = input.Description
            };
        }

        public static Brand ToBrand(this InputUpdateBrand input)
        {
            return new Brand
            {
                Name = input.Name,
                Code = input.Code,
                Description = input.Description
            };
        }

        public static List<OutputBrand> ToListOutputBrand(this List<Brand> brand)
        {
            return brand.Select(x => new OutputBrand(
                x.Id,
                x.Name,
                x.Code,
                x.Description
            )).ToList();
        }
    }
}