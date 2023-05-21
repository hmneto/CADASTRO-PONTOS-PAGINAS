// #nullable disable
using System.ComponentModel.DataAnnotations;


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

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O campo Nome deve ter no máximo 50 caracteres.")]
        public string ObservacaoPonto { get; set; }


        public virtual IconeDto Icone { get; set; }


        public virtual PaginaDto Pagina { get; set; }


        public int Zoom { get; set; }
    }
}


