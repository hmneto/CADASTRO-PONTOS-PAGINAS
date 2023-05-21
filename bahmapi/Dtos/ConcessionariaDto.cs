#nullable disable

using bahmapi.Entities;
namespace bahmapi.Dtos
{
    public class ConcessionariaDto
    {
        public int IdConcessionaria { get; set; }
        public string NomeConcessionaria { get; set; }
        public string InfoConcessionaria { get; set; }
        public DateTime DataHora { get; set; }

    }
}