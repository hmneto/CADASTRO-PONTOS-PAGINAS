#nullable disable
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
    public class PontoController : ControllerBase
    {
        private readonly AuthenticatedUser _user;
        public readonly IMapper _mapper;

        private readonly IPontoService _pontoService;

        private readonly ILogPontoService _logPontoService;
        public PontoController(
            IMapper mapper,
            AuthenticatedUser user
         )
        {
            _user = user;
            _pontoService = new PontoService();
            _pontoService = new PontoService();
            _logPontoService = new LogPontoService();
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Pontos")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult> Pontos([FromBody] PontoDto pontoDto)
        {
            Ponto ponto = _mapper.Map<Ponto>(pontoDto);
            List<PontoDto> ListaPontos = await _pontoService.Ponto(ponto, pontoDto.Zoom);
            _logPontoService.Registra(_user, ponto, pontoDto.Zoom, ListaPontos);
            return Ok(ListaPontos);
        }

        [HttpPost]
        [Route("Novo")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Novo(PontoDto pontoDto)
        {
            try
            {
                Ponto ponto = _mapper.Map<Ponto>(pontoDto);
                ponto.PontoUsuarioId = _user.Id;
                var ponto2 = await _pontoService.Novo(ponto);
                var pontoDto2 = _mapper.Map<PontoDto>(ponto2);
                return Ok(pontoDto2);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("Edita")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edita(PontoDto pontoDto)
        {
            try
            {
                pontoDto.PontoUsuarioId=_user.Id;
                Ponto ponto = await _pontoService.Detalhes(pontoDto.IdPonto);
                Ponto pontoModificado = _mapper.Map<PontoDto, Ponto>(pontoDto, ponto);
                pontoModificado = await _pontoService.Edita(pontoModificado);
                return Ok(_mapper.Map<PontoDto>(pontoModificado));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        [Route("Detalhes")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Detalhes([FromQuery] int id)
        {
            try
            {
                Ponto ponto = await _pontoService.Detalhes(id);
                PontoDto paginaDto = _mapper.Map<PontoDto>(ponto);
                return Ok(paginaDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListaTodos")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ListaTodos()
        {
            List<Ponto> pontos = await _pontoService.ListaTodos();
            List<PontoDto> usuariosDto = _mapper.Map<List<PontoDto>>(pontos);
            return Ok(usuariosDto);
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "admin")]
        public ActionResult Delete([FromQuery] int id)
        {
            try
            {
                return Ok(_pontoService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
