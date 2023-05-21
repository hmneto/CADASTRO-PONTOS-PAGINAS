using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
#nullable disable

namespace bahmapi.Entities
{
    public partial class Site
    {
        public Site()
        {
            PaginaSite = new HashSet<PaginaSite>();
        }

        public int IdSite { get; set; }
        public string NomeSite { get; set; }
        public string LinkSite { get; set; }
        public string TipoSite { get; set; }
        public int SiteUsuarioId { get; set; }

        public virtual Usuario SiteUsuario { get; set; }
        public virtual ICollection<PaginaSite> PaginaSite { get; set; }
    }
}
