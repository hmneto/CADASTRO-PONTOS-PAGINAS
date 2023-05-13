#nullable disable

using bahmapi.Entities;
namespace bahmapi.Dtos
{
    public class PontoDto
    {

        public int IdPonto { get; set; }
        public string NomePonto { get; set; }
        public float LatitudePonto { get; set; }
        public float LongitudePonto { get; set; }
        public int PaginaId { get; set; }
        public int IconeId { get; set; }
        public int PontoUsuarioId { get; set; }

        public virtual IconeDto Icone { get; set; }


        public virtual PaginaDto Pagina { get; set; }


        public int Zoom { get; set; }
    }
}