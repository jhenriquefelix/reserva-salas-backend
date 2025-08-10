using Microsoft.EntityFrameworkCore;
using ReservaSalas.Aplicacao.Abstracoes;
using ReservaSalas.Dominio.Entidades;

namespace ReservaSalas.Infraestrutura.Persistencia.Repos;

public class ReservasRepositorio : IReservasRepositorioLeitura, IReservasRepositorioEscrita
{
    private readonly ReservaSalasDbContext _db;
    public ReservasRepositorio(ReservaSalasDbContext db) => _db = db;

    public Task<bool> ExisteOverlapAsync(Guid salaId, DateTime inicio, DateTime fim, Guid? ignorarId, CancellationToken ct)
        => _db.Reservas.AsNoTracking()
            .Where(r => r.SalaId == salaId && (ignorarId == null || r.Id != ignorarId))
            .AnyAsync(r => r.Inicio < fim && inicio < r.Fim, ct);

    public async Task AdicionarAsync(Reserva entity, CancellationToken ct)
    {
        _db.Reservas.Add(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task AtualizarAsync(Reserva entity, byte[] rowVersion, CancellationToken ct)
    {
        // Define o valor original para checagem de concorrência
        _db.Entry(entity).Property(nameof(EntidadeBase.RowVersion)).OriginalValue = rowVersion;
        // Gera um novo token para a atualização (controle em código)
        _db.Entry(entity).Property(nameof(EntidadeBase.RowVersion)).CurrentValue = Guid.NewGuid().ToByteArray();
        _db.Reservas.Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task RemoverAsync(Guid id, CancellationToken ct)
    {
        var e = await _db.Reservas.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (e is null) return;
        _db.Reservas.Remove(e);
        await _db.SaveChangesAsync(ct);
    }

    public Task<Reserva?> ObterPorIdAsync(Guid id, CancellationToken ct)
        => _db.Reservas.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, ct);

    public async Task<IReadOnlyList<Reserva>> ListarAsync(Guid? salaId, DateTime? de, DateTime? ate, CancellationToken ct)
    {
        var q = _db.Reservas.AsNoTracking().AsQueryable();
        if (salaId.HasValue) q = q.Where(r => r.SalaId == salaId.Value);
        if (de.HasValue) q = q.Where(r => r.Inicio >= de.Value);
        if (ate.HasValue) q = q.Where(r => r.Fim <= ate.Value);
        return await q.OrderBy(r => r.Inicio).ToListAsync(ct);
    }
}
