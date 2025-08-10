using ReservaSalas.Aplicacao.DTOs.Reserva;
using ReservaSalas.Dominio.Entidades;

namespace ReservaSalas.Aplicacao.Abstracoes
{
    public interface IReservasRepositorioLeitura
    {
        Task<bool> ExisteOverlapAsync(Guid salaId, DateTime inicio, DateTime fim, Guid? ignorarId, CancellationToken ct);
        Task<Reserva?> ObterPorIdAsync(Guid id, CancellationToken ct);
        Task<IReadOnlyList<Reserva>> ListarAsync(Guid? salaId, DateTime? de, DateTime? ate, CancellationToken ct);
    }
}
