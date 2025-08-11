using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaSalas.Aplicacao.DTOs.Sala
{
    public record SalaDto(Guid Id, string Nome, Guid LocalId);

}
