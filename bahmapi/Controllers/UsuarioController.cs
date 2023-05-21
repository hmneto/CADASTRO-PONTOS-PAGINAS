#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using bahmapi.Entities;
using bahmapi.Dtos;
using bahmapi.Services;
using AutoMapper;
namespace bahmapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsuarioController : Controller
    {

        public readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        private readonly TokenService _tokenService;

        private readonly EmailService _emailService;
        public UsuarioController(
            IMapper mapper,
            TokenService tokenService,
            IUsuarioService usuarioService,
            EmailService emailService
         )
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _usuarioService = usuarioService;
            _emailService = emailService;
        }



        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody] UsuarioDto usuarioDto)
        {

            // try
            // {
            //     Usuario usuario = _usuarioService.Login(usuarioDto.EmailUsuario, usuarioDto.SenhaUsuario);
            //     _emailService.EnviaEmail("", "", "Login", "usuario " + usuario.EmailUsuario + " logado");

            // }catch
            // {
                
            // }



            try
            {
                Usuario usuario = _usuarioService.Login(usuarioDto.EmailUsuario, usuarioDto.SenhaUsuario);
                string token = _tokenService.GenerateToken(usuario);
                usuarioDto = _mapper.Map<UsuarioDto>(usuario);
                usuarioDto.Token = token;
                return Ok(usuarioDto);
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }


        [HttpPost]
        [Route("Novo")]
        [AllowAnonymous]
        public ActionResult Novo([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                Usuario usuario = _mapper.Map<Usuario>(usuarioDto);
                usuario = _usuarioService.Novo(usuario);
                return Ok(usuario);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpPut]
        [Route("Edita")]
        public async Task<ActionResult> Edita([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                Usuario usuario = await _usuarioService.Detalhes(usuarioDto.IdUsuario);

                Usuario usuarioModificado = _mapper.Map<UsuarioDto, Usuario>(usuarioDto, usuario);
                usuarioModificado = _usuarioService.Edita(usuarioModificado);
                return Ok(usuarioModificado);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }



        [HttpGet]
        [Route("Detalhes")]
        public async Task<ActionResult> Detalhes([FromQuery] int id)
        {
            try
            {
                Usuario usuario = await _usuarioService.Detalhes(id);
                UsuarioDto usuarioDto = _mapper.Map<UsuarioDto>(usuario);
                return Ok(usuarioDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListaTodos")]

        public async Task<ActionResult> ListaTodos()
        {
            List<Usuario> usuarios = await _usuarioService.ListaTodos();
            List<UsuarioDto> usuariosDto = _mapper.Map<List<UsuarioDto>>(usuarios);
            return Ok(usuariosDto);
        }



        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete([FromQuery] int id)
        {
            try
            {
                return Ok(_usuarioService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}

