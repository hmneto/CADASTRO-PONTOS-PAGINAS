using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
#nullable disable

namespace bahmapi.Entities
{
    public partial class PaginaSite
    {
        public int IdPaginaSite { get; set; }
        public int SiteId { get; set; }
        public int PaginaId { get; set; }

        public virtual Pagina Pagina { get; set; }
        public virtual Site Site { get; set; }
    }
}
