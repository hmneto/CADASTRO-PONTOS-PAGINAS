using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace bahmapi.Entities
{
    public partial class Ponto
    {
        public int IdPonto { get; set; }
        public string NomePonto { get; set; }
        public float LatitudePonto { get; set; }
        public float LongitudePonto { get; set; }
        public int PaginaId { get; set; }
        public int IconeId { get; set; }
        public int PontoUsuarioId { get; set; }
        public string ObservacaoPonto { get; set; }

        public virtual Icone Icone { get; set; }
        public virtual Pagina Pagina { get; set; }
        public virtual Usuario PontoUsuario { get; set; }
    }
}
