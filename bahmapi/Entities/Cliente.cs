using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace bahmapi.Entities
{
    public partial class Cliente
    {
        public Cliente()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string IpCliente { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
