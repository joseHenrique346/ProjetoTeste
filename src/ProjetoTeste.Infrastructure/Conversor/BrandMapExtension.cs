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
            return new Brand(
                input.Name,
                input.Code,
                input.Description
            );
        }

        public static List<Brand> ToListBrand(this List<InputCreateBrand> input)
        {
            return input.Select(i => new Brand(
                i.Name,
                i.Code,
                i.Description
            )).ToList();
        }

        public static List<OutputBrand?>? ToListOutputBrand(this List<Brand>? brand)
        {
            return brand != null ? brand.Select(x => x.ToOutputBrand()).ToList() : null;
        }
    }
}