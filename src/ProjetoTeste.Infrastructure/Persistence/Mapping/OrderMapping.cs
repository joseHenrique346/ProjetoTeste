using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Persistence.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Customer).WithMany(y => y.Order).HasForeignKey(x => x.CustomerId);

            builder.ToTable("pedido");

            builder.Property(x => x.OrderDate).HasColumnName("data_de_criacao");
            builder.Property(x => x.OrderDate).IsRequired();
        }
    }
}