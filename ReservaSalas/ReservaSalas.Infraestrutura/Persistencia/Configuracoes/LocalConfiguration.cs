using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservaSalas.Dominio.Entidades;

namespace ReservaSalas.Infraestrutura.Persistencia.Configuracoes
{
    public class LocalConfiguration : IEntityTypeConfiguration<Local>
    {
        public void Configure(EntityTypeBuilder<Local> builder)
        {
            builder.ToTable("Locais");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                   .HasMaxLength(120)
                   .IsRequired();

            builder.HasIndex(x => x.Nome)
                   .IsUnique();
        }
    }
}
