using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace bahmapi.Entities
{
    public partial class Usuario
    {
        public Usuario()
        {
            LogMapa = new HashSet<LogMapa>();
            LogPagina = new HashSet<LogPagina>();
            LogPonto = new HashSet<LogPonto>();
            Pagina = new HashSet<Pagina>();
            Ponto = new HashSet<Ponto>();
            Site = new HashSet<Site>();
        }

        public int IdUsuario { get; set; }
        public int ClienteId { get; set; }
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public string PerfilUsuario { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<LogMapa> LogMapa { get; set; }
        public virtual ICollection<LogPagina> LogPagina { get; set; }
        public virtual ICollection<LogPonto> LogPonto { get; set; }
        public virtual ICollection<Pagina> Pagina { get; set; }
        public virtual ICollection<Ponto> Ponto { get; set; }
        public virtual ICollection<Site> Site { get; set; }
    }
}
