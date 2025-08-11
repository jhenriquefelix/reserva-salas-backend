using ReservaSalas.Aplicacao.Abstracoes;
using ReservaSalas.Aplicacao.DTOs.Local;
using ReservaSalas.Aplicacao.DTOs.Sala;

namespace ReservaSalas.Aplicacao.Servico
{

    public class LocaisAppService
    {
        private readonly ILocaisRepositorioLeitura _read;

        public LocaisAppService(ILocaisRepositorioLeitura read)
        {
            _read = read;
        }

        public Task<IReadOnlyList<LocalDto>> ListarLocaisAsync(CancellationToken ct) =>
            _read.GetLocaisAsync(ct);

        public Task<IReadOnlyList<SalaDto>> ListarSalasPorLocalAsync(Guid localId, CancellationToken ct) =>
            _read.GetSalasPorLocalAsync(localId, ct);
    }

}
