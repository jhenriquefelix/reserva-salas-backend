using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservaSalas.Dominio.Entidades;

namespace ReservaSalas.Infraestrutura.Persistencia.Configuracoes
{
    public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.ToTable("Reservas");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ResponsavelNome)
                   .HasMaxLength(120)
                   .IsRequired();

            builder.Property(x => x.ResponsavelEmail)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Inicio).IsRequired();
            builder.Property(x => x.Fim).IsRequired();

            builder.HasOne(r => r.Local)
                   .WithMany()
                   .HasForeignKey(r => r.LocalId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Sala)
                   .WithMany(s => s.Reservas)
                   .HasForeignKey(r => r.SalaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.SalaId, x.Inicio });
            builder.HasIndex(x => new { x.SalaId, x.Fim });

            builder.HasCheckConstraint("CK_Reservas_InicioMenorQueFim", "Inicio < Fim");
        }
    }
}
