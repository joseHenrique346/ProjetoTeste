namespace ProjetoTeste.Api.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using ProjetoTeste.Infrastructure.Interface.UnitOfWork;
    using ProjetoTeste.Infrastructure.Persistence.Repositories;
    using ProjetoTeste.Infrastructure.Service;

    public class InjectionDependencyExtension
    {
        public static IServiceCollection ConfigureAddition(this IServiceCollection services)
        {
            services.AddScoped<BrandService>();
            services.AddScoped<ClientService>();
            services.AddScoped<ProductService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
