// #nullable disable
using bahmapi.Entities;
using bahmapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using AutoMapper;
using bahmapi.Dtos;

namespace bahmapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ApiMapsController : ControllerBase
    {
        private readonly AuthenticatedUser user;
        public readonly IMapper mapper;
        private readonly IClienteService clienteService;
        private readonly IUsuarioService usuarioService;

        public ApiMapsController(
            IMapper _mapper,
            AuthenticatedUser _user,
            IClienteService _clienteService,
            IUsuarioService _usuarioService
         )
        {
            user = _user;
            mapper = _mapper;
            clienteService = _clienteService;
            usuarioService = _usuarioService;
        }



        [HttpGet]
        [Route("Google")]
        public async Task<ActionResult> Google()
        {
            var usuario = await usuarioService.Detalhes(user.Id);
            var cliente = await clienteService.Detalhes(usuario.ClienteId);
            return Ok(new {ApiMaps=cliente.ChaveGoogleMaps.Trim()});
        }

        [HttpGet]
        [Route("MapsCount")]
        public async Task<ActionResult> MapsCount()
        {
            var usuario = await usuarioService.Detalhes(user.Id);
            //var cliente = await clienteService.Detalhes(usuario.ClienteId);
            // return Ok(new {ApiMaps=cliente.ChaveGoogleMaps.Trim()});
            return Ok();
        }
    }
}
