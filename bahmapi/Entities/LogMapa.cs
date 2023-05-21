using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
#nullable disable

namespace bahmapi.Entities
{
    public partial class LogMapa
    {
        public int IdLogMapa { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataHoraLogMapa { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
