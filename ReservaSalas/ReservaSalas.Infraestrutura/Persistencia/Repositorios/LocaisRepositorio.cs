using Microsoft.EntityFrameworkCore;
using ReservaSalas.Aplicacao.Abstracoes;
using ReservaSalas.Aplicacao.DTOs.Local;
using ReservaSalas.Aplicacao.DTOs.Sala;

namespace ReservaSalas.Infraestrutura.Persistencia.Repos;

public class LocaisRepositorio : ILocaisRepositorioLeitura
{
    private readonly ReservaSalasDbContext _db;
    public LocaisRepositorio(ReservaSalasDbContext db) => _db = db;

    public async Task<IReadOnlyList<LocalDto>> GetLocaisAsync(CancellationToken ct) =>
        await _db.Locais
            .AsNoTracking()
            .OrderBy(l => l.Nome)
            .Select(l => new LocalDto(l.Id, l.Nome))
            .ToListAsync(ct);

    public async Task<IReadOnlyList<SalaDto>> GetSalasPorLocalAsync(Guid localId, CancellationToken ct) =>
        await _db.Salas
            .AsNoTracking()
            .Where(s => s.LocalId == localId)
            .OrderBy(s => s.Nome)
            .Select(s => new SalaDto(s.Id, s.Nome, s.LocalId))
            .ToListAsync(ct);
}
