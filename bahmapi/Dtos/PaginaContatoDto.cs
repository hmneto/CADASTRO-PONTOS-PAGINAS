using System;
using System.Collections.Generic;
using bahmapi.Dtos;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
#nullable disable

namespace bahmapi.Entities
{
    public partial class PaginaContatoDto
    {
        public int IdPaginaContato { get; set; }
        public int ContatoId { get; set; }
        public int PaginaId { get; set; }

        public virtual ContatoDto Contato { get; set; }
    }
}
