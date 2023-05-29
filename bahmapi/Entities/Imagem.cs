using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace bahmapi.Entities
{
    public partial class Imagem
    {
        public int IdImagem { get; set; }
        public string NomeImagem { get; set; }
        public byte[] BinarioImagem { get; set; }
        public string ExtensaoImagem { get; set; }
    }
}
