using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
#nullable disable

namespace bahmapi.Entities
{
    public partial class LogPagina
    {
        public int IdLogPagina { get; set; }
        public int UsuarioId { get; set; }
        public int PaginaId { get; set; }
        public DateTime DataHora { get; set; }

        public virtual Pagina Pagina { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
