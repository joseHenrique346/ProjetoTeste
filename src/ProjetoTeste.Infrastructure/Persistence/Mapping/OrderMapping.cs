using ProjetoTeste.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjetoTeste.Infrastructure.Persistence.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("pedido");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Customer)
                .WithMany(y => y.Order)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Property(x => x.CreatedDate).HasColumnName("data_de_criacao");
            builder.Property(x => x.CreatedDate).IsRequired();
        }
    }
}