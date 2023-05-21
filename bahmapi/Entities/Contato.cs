using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace bahmapi.Entities
{
    public partial class Contato
    {
        public Contato()
        {
            PaginaContato = new HashSet<PaginaContato>();
        }

        public int IdContato { get; set; }
        public string InfoContato { get; set; }

        public virtual ICollection<PaginaContato> PaginaContato { get; set; }
    }
}
