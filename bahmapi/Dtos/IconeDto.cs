// #nullable disable

using bahmapi.Entities;
namespace bahmapi.Dtos
{
    public class IconeDto
    {
        public int IdIcone { get; set; }
        public string NomeIcone { get; set; }
        public string LinkIcone { get; set; }
        public string AcaoIcone { get; set; }
        public DateTime DataHora { get; set; }
    }
}