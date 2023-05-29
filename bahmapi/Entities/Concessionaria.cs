using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace bahmapi.Entities
{
    public partial class Concessionaria
    {
        public Concessionaria()
        {
            Pagina = new HashSet<Pagina>();
        }

        public int IdConcessionaria { get; set; }
        public string NomeConcessionaria { get; set; }
        public string InfoConcessionaria { get; set; }

        public virtual ICollection<Pagina> Pagina { get; set; }
    }
}
