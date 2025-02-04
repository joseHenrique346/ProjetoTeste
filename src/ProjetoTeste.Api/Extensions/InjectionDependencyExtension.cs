namespace ProjetoTeste.Api.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using ProjetoTeste.Infrastructure.Interface.Repository;
    using ProjetoTeste.Infrastructure.Interface.Service;
    using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
    using ProjetoTeste.Infrastructure.Mapper;
    using ProjetoTeste.Infrastructure.Persistence;
    using ProjetoTeste.Infrastructure.Persistence.Repositories;
    using ProjetoTeste.Infrastructure.Service;

    internal static class InjectionDependencyExtension
    {
        public static IServiceCollection ConfigureInjectionDependency(this IServiceCollection services)
        {
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderValidateService, OrderValidateService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductValidateService, ProductValidateService>();
            services.AddScoped<IProductOrderRepository, ProductOrderRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IBrandValidateService, BrandValidateService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerValidateService, CustomerValidateService>();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}