namespace ReservaSalas.Aplicacao.DTOs.Reserva
{
    public record ReservaDto(
        Guid Id,
        Guid LocalId,
        Guid SalaId,
        string LocalNome,
        string SalaNome,
        string ResponsavelNome,
        string ResponsavelEmail,
        DateTime Inicio,
        DateTime Fim,
        bool Cafe,
        int? CafeQuantidade,
        string? CafeDescricao,
        byte[] RowVersion
    );
}
