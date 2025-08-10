using System.ComponentModel.DataAnnotations;

namespace ReservaSalas.Dominio.Entidades
{
    public class Sala : EntidadeBase
    {
        [Required]
        public Guid LocalId { get; private set; }

        [Required, MaxLength(120)]
        public string Nome { get; private set; } = null!;

        public int Capacidade { get; private set; }

        // Navegação
        public virtual Local Local { get; private set; } = null!;
        public virtual ICollection<Reserva> Reservas { get; private set; } = new List<Reserva>();

        private Sala() { } // EF

        public Sala(Guid localId, string nome, int capacidade)
        {
            LocalId = localId;
            Nome = nome.Trim();
            Capacidade = capacidade;
            // Define token de concorrência inicial
            RowVersion = Guid.NewGuid().ToByteArray();
        }
    }
}
