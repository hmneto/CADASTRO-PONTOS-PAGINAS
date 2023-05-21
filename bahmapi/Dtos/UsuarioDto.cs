#nullable disable


namespace bahmapi.Dtos
{
    public class UsuarioDto
    {


        public int IdUsuario { get; set; }
        public int ClienteId { get; set; }
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public string IpUsuario { get; set; }
        public string PerfilUsuario { get; set; }



        //dto
        public string Token { get; set; }


    }
}