#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using bahmapi.Entities;
using bahmapi.Dtos;
using bahmapi.Services;
using AutoMapper;
using System.Text;

namespace bahmapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ATesteController : Controller
    {


        public IUsuarioService usuarioService;
        public IClienteService clienteService;
        public IIconeService iconeService;
        public IConcessionariaService concessionariaService;
        public IPaginaService paginaService;
        private IImagemService imagemService;
        TokenService tokenService;
        public ATesteController(
            IUsuarioService _usuarioService,
            TokenService _tokenService,
            IIconeService _iconeService,
            IConcessionariaService _concessionariaService,
            IPaginaService _paginaService,
            IClienteService _clienteService,
            IImagemService _imagemService
         )
        {
            usuarioService = _usuarioService;
            tokenService = _tokenService;
            iconeService = _iconeService;
            concessionariaService = _concessionariaService;
            paginaService = _paginaService;
            clienteService = _clienteService;
            imagemService = _imagemService;
        }


        [HttpGet]
        [Route("Setap")]
        [AllowAnonymous]
        public async Task<ActionResult> Setap()
        {


            DatabaseContext BahmDbContext = new DatabaseContext();
            string caminho = @"../imagens";

            foreach (string item in Directory.GetFiles(caminho))
            {
                try
                {
                    var data = System.IO.File.ReadAllBytes(caminho + item.Remove(0, caminho.Length));

                    var fi = new System.IO.FileInfo(caminho + item.Remove(0, caminho.Length));
                    Console.WriteLine(fi.Name);
                    BahmDbContext.Imagem.Add(new Imagem
                    {
                        BinarioImagem = data,
                        ExtensaoImagem = fi.Extension,
                        NomeImagem = fi.Name,
                        TamanhoImagem = fi.Length
                    });
                    await BahmDbContext.SaveChangesAsync();

                }
                catch (System.IO.FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }




            var cliente = await clienteService.Novo(new Cliente{
                NomeCliente="Cliente1",
                IpCliente="0.0.0.1",
                ChaveGoogleMaps="AIzaSyB41DRUbKWJHPxaFjMAwdrzWzbVKartNGg" 
            });


            var usuario = usuarioService.Novo(new Usuario
            {
                EmailUsuario = "admin",
                SenhaUsuario = "123",
                PerfilUsuario = "admin",
                ClienteId = cliente.IdCliente
            });



            usuarioService.Novo(new Usuario
            {
                EmailUsuario = "user",
                SenhaUsuario = "123",
                PerfilUsuario = "user",
                ClienteId = cliente.IdCliente
            });



            await iconeService.Novo(new Icone
            {
                NomeIcone = "motel",
                LinkIcone = "MOTEL.png",
                AcaoIcone = "SIM"
            });


            
            await iconeService.Novo(new Icone
            {
                NomeIcone = "tunnel",
                LinkIcone = "TUN.png",
                AcaoIcone = "SIM"
            });

            await paginaService.Novo(new Pagina
            {
                NomePagina = "TESTE",
                EnderecoPagina = "TESTE",
                Concessionaria = new Concessionaria
                {
                    NomeConcessionaria = "teste",
                    InfoConcessionaria = "teste"
                }
            });

            return Ok(tokenService.GenerateToken(usuario));
        }



        [HttpGet]
        [Route("Teste")]
        [AllowAnonymous]
        public ActionResult Teste()
        {
            return Ok("Teste");
        }


        [HttpGet]
        [Route("Foto")]
        [Authorize]
        public async Task<IActionResult> Foto(int id)
        {
            try
            {
                Imagem imagem2 = await imagemService.Detalhes(id);
                string fotoBase64 = Encoding.UTF8.GetString(imagem2.BinarioImagem);
                String[] substrings = fotoBase64.Split(',');
                string header = substrings[0];
                string imagem = substrings[1];
                return File(imagem2.BinarioImagem, "image/png");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }



        [HttpGet]
        [Route("Import")]
        [AllowAnonymous]
        public async Task<ActionResult> Import(string i)
        {
            DatabaseContext BahmDbContext = new DatabaseContext();
            string caminho = @"../imagens";

            foreach (string item in Directory.GetFiles(caminho))
            {
                try
                {
                    var data = System.IO.File.ReadAllBytes(caminho + item.Remove(0, caminho.Length));

                    var fi = new System.IO.FileInfo(caminho + item.Remove(0, caminho.Length));
                    Console.WriteLine(fi.Name);
                    BahmDbContext.Imagem.Add(new Imagem
                    {
                        BinarioImagem = data,
                        ExtensaoImagem = fi.Extension,
                        NomeImagem = fi.Name,
                        TamanhoImagem = fi.Length
                    });
                    await BahmDbContext.SaveChangesAsync();

                }
                catch (System.IO.FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
            return Ok();
        }





        [HttpGet]
        [Route("Teste2")]
        [AllowAnonymous]
        public ActionResult Teste2()
        {
            DatabaseContext db = new DatabaseContext();

            var contatos = db.Contato.ToList();
            foreach(var contato in contatos)
            {
                contato.InfoContato = $"{(string.IsNullOrEmpty(contato.InfoContato)? "": contato.InfoContato )}";
            }
            db.SaveChanges();
            return Ok("Teste");
        }
    }


}

