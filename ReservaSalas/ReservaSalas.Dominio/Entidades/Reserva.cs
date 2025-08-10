using System.ComponentModel.DataAnnotations;

namespace ReservaSalas.Dominio.Entidades
{
    public class Reserva : EntidadeBase
    {
        [Required]
        public Guid LocalId { get; private set; }

        [Required]
        public Guid SalaId { get; private set; }

        [Required, MaxLength(120)]
        public string ResponsavelNome { get; private set; } = null!;

        [Required, MaxLength(200), EmailAddress]
        public string ResponsavelEmail { get; private set; } = null!;

        [Required]
        public DateTime Inicio { get; private set; }

        [Required]
        public DateTime Fim { get; private set; }

        public bool Cafe { get; private set; }

        [Range(1, int.MaxValue)]
        public int? CafeQuantidade { get; private set; }

        [MaxLength(300)]
        public string? CafeDescricao { get; private set; }

        // Navegações
        public virtual Local Local { get; private set; } = null!;
        public virtual Sala Sala { get; private set; } = null!;

        private Reserva() { } // EF

        public Reserva(Guid localId, Guid salaId, string nome, string email,
                       DateTime inicio, DateTime fim,
                       bool cafe, int? cafeQuantidade, string? cafeDescricao)
        {
            LocalId = localId;
            SalaId = salaId;
            ResponsavelNome = nome.Trim();
            ResponsavelEmail = email.Trim();
            Inicio = inicio;
            Fim = fim;
            Cafe = cafe;
            CafeQuantidade = cafe ? cafeQuantidade : null;
            CafeDescricao = cafe ? cafeDescricao : null;
            // Define token de concorrência inicial (controlado em código)
            RowVersion = Guid.NewGuid().ToByteArray();
        }
    }

}
