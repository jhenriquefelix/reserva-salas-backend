using Microsoft.EntityFrameworkCore;
using ReservaSalas.Dominio.Entidades;

namespace ReservaSalas.Infraestrutura.Persistencia;

public class ReservaSalasDbContext : DbContext
{
    public ReservaSalasDbContext(DbContextOptions<ReservaSalasDbContext> options) : base(options) { }

    public DbSet<Local> Locais => Set<Local>();
    public DbSet<Sala> Salas => Set<Sala>();
    public DbSet<Reserva> Reservas => Set<Reserva>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReservaSalasDbContext).Assembly);

    }


}
