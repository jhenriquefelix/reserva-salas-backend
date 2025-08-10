namespace ReservaSalas.Aplicacao.DTOs.Reserva
{
    public record CriarReservaDto(
    Guid LocalId,
    Guid SalaId,
    string ResponsavelNome,
    string ResponsavelEmail,
    DateTime Inicio,
    DateTime Fim,
    bool Cafe,
    int? CafeQuantidade,
    string? CafeDescricao
    );
}
