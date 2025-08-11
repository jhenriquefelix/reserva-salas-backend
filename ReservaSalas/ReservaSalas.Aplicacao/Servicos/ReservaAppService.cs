using FluentValidation;
using ReservaSalas.Aplicacao.Abstracoes;    
using ReservaSalas.Aplicacao.DTOs.Reserva;         
using ReservaSalas.Dominio.Entidades;

namespace ReservaSalas.Aplicacao.Servico
{
    public class ReservaAppService
    {
        private readonly IReservasRepositorioLeitura _read;
        private readonly IReservasRepositorioEscrita _write;
        private readonly IValidator<CriarReservaDto> _createVal;
        private readonly IValidator<AtualizarReservaDto> _updateVal;

        public ReservaAppService(
            IReservasRepositorioLeitura read,
            IReservasRepositorioEscrita write,
            IValidator<CriarReservaDto> createVal,
            IValidator<AtualizarReservaDto> updateVal)
        {
            _read = read; _write = write; _createVal = createVal; _updateVal = updateVal;
        }

        public async Task<Guid> CriarAsync(CriarReservaDto dto, CancellationToken ct)
        {
            await _createVal.ValidateAndThrowAsync(dto, ct);

            var conflito = await _read.ExisteOverlapAsync(dto.SalaId, dto.Inicio, dto.Fim, null, ct);
            if (conflito) throw new InvalidOperationException("Conflito de horário para a sala.");

            var entity = new Reserva(dto.LocalId, dto.SalaId, dto.ResponsavelNome, dto.ResponsavelEmail,
                                     dto.Inicio, dto.Fim, dto.Cafe, dto.CafeQuantidade, dto.CafeDescricao);

            await _write.AdicionarAsync(entity, ct);
            return entity.Id;
        }

        public async Task AtualizarAsync(AtualizarReservaDto dto, CancellationToken ct)
        {
            await _updateVal.ValidateAndThrowAsync(dto, ct);

            var conflito = await _read.ExisteOverlapAsync(dto.SalaId, dto.Inicio, dto.Fim, dto.Id, ct);
            if (conflito) throw new InvalidOperationException("Conflito de horário para a sala.");

            var entity = new Reserva(dto.LocalId, dto.SalaId, dto.ResponsavelNome, dto.ResponsavelEmail,
                                     dto.Inicio, dto.Fim, dto.Cafe, dto.CafeQuantidade, dto.CafeDescricao);

            // manter identidade para o update
            typeof(EntidadeBase).GetProperty("Id")!.SetValue(entity, dto.Id);

            await _write.AtualizarAsync(entity, dto.RowVersion, ct);
        }

        public async Task<ReservaDto?> ObterAsync(Guid id, CancellationToken ct)
        {
            var e = await _read.ObterPorIdAsync(id, ct);   // entidade do Domínio
            return e is null ? null : ToDto(e);            // mapeia para DTO
        }

        public async Task<IReadOnlyList<ReservaDto>> ListarAsync(Guid? salaId, DateTime? de, DateTime? ate, CancellationToken ct)
        {
            var list = await _read.ListarAsync(salaId, de, ate, ct); // lista de entidades
            return list.Select(ToDto).ToList();                      // mapeia para DTO
        }

        public Task RemoverAsync(Guid id, CancellationToken ct)
          => _write.RemoverAsync(id, ct);

        private static ReservaDto ToDto(Reserva r) =>
            new ReservaDto(
                r.Id,
                r.LocalId,
                r.SalaId,
                r.Local?.Nome ?? string.Empty,
                r.Sala?.Nome ?? string.Empty,
                r.ResponsavelNome,
                r.ResponsavelEmail,
                r.Inicio,
                r.Fim,
                r.Cafe,
                r.CafeQuantidade,
                r.CafeDescricao,
                r.RowVersion
            );
    }


    }
