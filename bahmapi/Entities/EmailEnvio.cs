using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace bahmapi.Entities
{
    public partial class EmailEnvio
    {
        public int IdEmailEnviado { get; set; }
        public string Acao { get; set; }
        public string Host { get; set; }
        public ulong Ssl { get; set; }
        public int Porta { get; set; }
        public string Usuario { get; set; }
        public int Senha { get; set; }
        public string Email { get; set; }
    }
}
