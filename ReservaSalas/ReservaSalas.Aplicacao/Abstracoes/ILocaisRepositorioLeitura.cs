using ReservaSalas.Aplicacao.DTOs.Local;
using ReservaSalas.Aplicacao.DTOs.Sala;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSalas.Aplicacao.Abstracoes
{
    public interface ILocaisRepositorioLeitura
    {
        Task<IReadOnlyList<LocalDto>> GetLocaisAsync(CancellationToken ct);
        Task<IReadOnlyList<SalaDto>> GetSalasPorLocalAsync(Guid localId, CancellationToken ct);
    }
}
