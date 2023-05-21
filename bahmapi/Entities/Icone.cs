using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
#nullable disable

namespace bahmapi.Entities
{
    public partial class Icone
    {
        public Icone()
        {
            Ponto = new HashSet<Ponto>();
        }

        public int IdIcone { get; set; }
        public string NomeIcone { get; set; }
        public string LinkIcone { get; set; }
        public string AcaoIcone { get; set; }

        public virtual ICollection<Ponto> Ponto { get; set; }
    }
}
