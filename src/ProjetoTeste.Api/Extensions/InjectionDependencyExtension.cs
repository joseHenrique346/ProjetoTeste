namespace ProjetoTeste.Api.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using ProjetoTeste.Infrastructure.Interface.Repository;
    using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
    using ProjetoTeste.Infrastructure.Persistence.Repositories;
    using ProjetoTeste.Infrastructure.Service;

    internal static class InjectionDependencyExtension
    {
        public static IServiceCollection ConfigureInjectionDependency(this IServiceCollection services)
        {
            services.AddScoped<BrandService>();
            services.AddScoped<ClientService>();
            services.AddScoped<ProductService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
