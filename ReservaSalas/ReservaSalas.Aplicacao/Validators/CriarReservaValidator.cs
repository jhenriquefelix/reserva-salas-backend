using FluentValidation;
using ReservaSalas.Aplicacao.DTOs.Reserva;

namespace ReservaSalas.Aplicacao.Validators
{
    public class CriarReservaValidator : AbstractValidator<CriarReservaDto>
    {
        public CriarReservaValidator()
        {
            RuleFor(x => x.LocalId).NotEmpty();
            RuleFor(x => x.SalaId).NotEmpty();
            RuleFor(x => x.ResponsavelNome).NotEmpty().MaximumLength(120);
            RuleFor(x => x.ResponsavelEmail).NotEmpty().EmailAddress().MaximumLength(200);
            RuleFor(x => x.Fim).GreaterThan(x => x.Inicio)
                .WithMessage("Fim deve ser maior que Início.");
            When(x => x.Cafe, () =>
            {
                RuleFor(x => x.CafeQuantidade).NotNull().GreaterThan(0);
            });
        }
    }
}
