using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace bahmapi.Entities
{
    public partial class PaginaContato
    {
        public int IdPaginaContato { get; set; }
        public int ContatoId { get; set; }
        public int PaginaId { get; set; }

        public virtual Contato Contato { get; set; }
        public virtual Pagina Pagina { get; set; }
    }
}
