using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Persistence.Mapping
{
    public class ProductOrderMapping : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasKey(x => new { x.OrderId, x.ProductId });
            builder.HasOne(x => x.Order).WithMany(y => y.ProductOrders).HasForeignKey(x => x.OrderId);
            builder.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);

            builder.ToTable("pedido_de_produto");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.OrderId).HasColumnName("pedido_id");
            builder.Property(p => p.OrderId).IsRequired();

            builder.Property(p => p.ProductId).HasColumnName("produto_id");
            builder.Property(p => p.ProductId).IsRequired();

            builder.Property(p => p.Quantity).HasColumnName("quantidade");
            builder.Property(p => p.Quantity).IsRequired();

            builder.Property(p => p.UnitPrice).HasColumnName("preco_unitario");
            builder.Property(p => p.UnitPrice).IsRequired();

            builder.Property(p => p.TotalPrice).HasColumnName("subtotal");
            builder.Property(p => p.TotalPrice).IsRequired();
        }
    }
}