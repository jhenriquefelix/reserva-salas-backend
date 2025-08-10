using System.ComponentModel.DataAnnotations;

namespace ReservaSalas.Dominio.Entidades
{
    public class Local : EntidadeBase
    {
        [Required, MaxLength(120)]
        public string Nome { get; private set; } = null!;

        // Navegação (virtual se quiser lazy loading)
        public virtual ICollection<Sala> Salas { get; private set; } = new List<Sala>();

        private Local() { } // EF

        public Local(string nome)
        {
            Nome = nome.Trim();
            // Define token de concorrência inicial
            RowVersion = Guid.NewGuid().ToByteArray();
        }
    }
}
