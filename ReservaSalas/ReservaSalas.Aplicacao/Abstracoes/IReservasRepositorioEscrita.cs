using ReservaSalas.Dominio.Entidades;

namespace ReservaSalas.Aplicacao.Abstracoes
{
    public interface IReservasRepositorioEscrita
    {
        Task AdicionarAsync(Reserva entity, CancellationToken ct);
        Task AtualizarAsync(Reserva entity, byte[] rowVersion, CancellationToken ct);
        Task RemoverAsync(Guid id, CancellationToken ct);
    }
}
