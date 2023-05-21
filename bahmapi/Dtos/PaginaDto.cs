#nullable disable

using bahmapi.Entities;
namespace bahmapi.Dtos
{
    public class PaginaDto
    {
        public int IdPagina { get; set; }
        public string NomePagina { get; set; }
        public string EnderecoPagina { get; set; }
        public int ConcessionariaId { get; set; }
        public int PaginaUsuarioId { get; set; }


        // //dtos
        public List<SiteDto> ListSiteDto { get; set; }
        public List<ContatoDto> ListContatoDto { get; set; }
        public ConcessionariaDto Concessionaria { get; set; }

    }
}