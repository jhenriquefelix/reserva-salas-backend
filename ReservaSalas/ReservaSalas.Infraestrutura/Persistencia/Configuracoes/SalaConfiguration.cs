using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservaSalas.Dominio.Entidades;

namespace ReservaSalas.Infraestrutura.Persistencia.Configuracoes
{
    public class SalaConfiguration : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.ToTable("Salas");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                   .HasMaxLength(120)
                   .IsRequired();

            builder.HasCheckConstraint("CK_Salas_Capacidade_NaoNegativa", "Capacidade >= 0");

            builder.HasOne(s => s.Local)
                   .WithMany(l => l.Salas)
                   .HasForeignKey(s => s.LocalId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.LocalId, x.Nome }).IsUnique();
        }
    }
}
