using AutoMapper;
using ProjetoTeste.Arguments.Arguments;
using ProjetoTeste.Arguments.Arguments.Brand;
using ProjetoTeste.Arguments.Arguments.Customer;
using ProjetoTeste.Arguments.Arguments.Order;
using ProjetoTeste.Arguments.Arguments.Product;
using ProjetoTeste.Arguments.Arguments.ProductOrder;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, OutputBrand>();
            CreateMap<Brand, BrandDTO>().ReverseMap();
            CreateMap<Brand, OutputBrandWithProducts>();

            CreateMap<Product, OutputProduct>();
            CreateMap<Product, ProductDTO>().ReverseMap();

            CreateMap<Customer, OutputCustomer>();
            CreateMap<Customer, CustomerDTO>().ReverseMap();

            CreateMap<Order, OutputOrder>()
                .ForMember(i => i.ProductOrders, opt => opt.MapFrom(src => src.ListProductOrder));

            CreateMap<ProductOrder, OutputProductOrder>();

            //CreateMap<Order, OutputOrder>();
        }
    }
}
