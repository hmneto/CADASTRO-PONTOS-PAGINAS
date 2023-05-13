using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
#nullable disable

namespace bahmapi.Entities
{
    public partial class Pagina
    {
        public Pagina()
        {
            LogPagina = new HashSet<LogPagina>();
            PaginaContato = new HashSet<PaginaContato>();
            PaginaSite = new HashSet<PaginaSite>();
            Ponto = new HashSet<Ponto>();
        }

        public int IdPagina { get; set; }
        public string NomePagina { get; set; }
        public string EnderecoPagina { get; set; }
        public int ConcessionariaId { get; set; }
        public int PaginaUsuarioId { get; set; }

        public virtual Concessionaria Concessionaria { get; set; }
        public virtual Usuario PaginaUsuario { get; set; }
        public virtual ICollection<LogPagina> LogPagina { get; set; }
        public virtual ICollection<PaginaContato> PaginaContato { get; set; }
        public virtual ICollection<PaginaSite> PaginaSite { get; set; }
        public virtual ICollection<Ponto> Ponto { get; set; }
    }
}
