using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTeste.Infrastructure.Persistence.Entities;

namespace ProjetoTeste.Infrastructure.Persistence.Mapping
{
    public class BrandMapping : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("marca");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnName("nome");
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(40);

            builder.Property(x => x.Code).HasColumnName("codigo");
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Code).HasMaxLength(6);

            builder.Property(x => x.Description).HasColumnName("descricao");
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100);
        }
    }
}