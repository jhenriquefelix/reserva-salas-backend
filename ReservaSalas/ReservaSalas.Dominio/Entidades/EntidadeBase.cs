using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaSalas.Dominio.Entidades
{
    public abstract class EntidadeBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }

        /// <summary>
        /// Usado pelo EF Core para controle de concorrência otimista.
        /// Atualiza a cada modificação no registro.
        /// </summary>
        [ConcurrencyCheck]
        public byte[] RowVersion { get; protected set; } = Array.Empty<byte>();
    }
}
