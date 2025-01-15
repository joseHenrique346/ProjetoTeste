using Microsoft.EntityFrameworkCore;
using ProjetoTeste.Infrastructure.Persistence.Entities;
using ProjetoTeste.Infrastructure.Persistence.Mapping;

namespace ProjetoTeste.Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ProductOrder> ProductOrder { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerMapping).Assembly);
        }
    }
}