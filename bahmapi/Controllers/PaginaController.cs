#nullable disable
using AutoMapper;
using bahmapi.Dtos;
using bahmapi.Entities;
using bahmapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace bahmapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PaginaController : Controller
    {
        private readonly AuthenticatedUser _user;

        public readonly IMapper _mapper;

        private readonly IPaginaService _paginaService;
        private readonly ILogPaginaService _logPaginaService;

        public PaginaController(
            IMapper mapper,
            AuthenticatedUser user
        )
        {
            _user = user;
            _paginaService = new PaginaService();
            _logPaginaService = new LogPaginaService();
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Pagina")]
        public async Task<ActionResult> Pagina([FromQuery] int PaginaId)
        {
            try
            {
                // var id_pagina2 = JsonWebToken.Decode(value, Settings.Secret);
                // JObject json = JObject.Parse(id_pagina2);
                // int id_pagina = (int)json["t"];

                PaginaDto paginaDto = await _paginaService.Pagina(PaginaId);
                _logPaginaService.Registra(_user, PaginaId);

                return Ok(paginaDto);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPost]
        [Route("Novo")]
        public async Task<ActionResult> Novo(PaginaDto paginaDto)
        {
            try
            {
                paginaDto.PaginaUsuarioId = _user.Id;
                Pagina pagina = _mapper.Map<Pagina>(paginaDto);

                foreach (var item in paginaDto.ListContatoDto)
                {
                    pagina.PaginaContato.Add(new PaginaContato
                    {
                        ContatoId = item.IdContato,
                        PaginaId = pagina.IdPagina
                    });
                }


                foreach (var item in paginaDto.ListSiteDto)
                {
                    pagina.PaginaSite.Add(new PaginaSite
                    {
                        SiteId = item.IdSite,
                        PaginaId = pagina.IdPagina
                    });
                }


                pagina = await _paginaService.Novo(pagina);
                return Ok(pagina);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("Edita")]
        public async Task<ActionResult> Edita(PaginaDto paginaDto)
        {
            try
            {
                paginaDto.PaginaUsuarioId = _user.Id;
                Pagina pagina = await _paginaService.Detalhes(paginaDto.IdPagina);

                Pagina paginaModificada = _mapper.Map<PaginaDto, Pagina>(paginaDto, pagina);

                foreach (var item in paginaDto.ListContatoDto)
                {
                    paginaModificada.PaginaContato.Add(new PaginaContato
                    {
                        ContatoId = item.IdContato,
                        PaginaId = pagina.IdPagina
                    });
                }


                foreach (var item in paginaDto.ListSiteDto)
                {
                    paginaModificada.PaginaSite.Add(new PaginaSite
                    {
                        SiteId = item.IdSite,
                        PaginaId = pagina.IdPagina
                    });
                }

                paginaModificada = await _paginaService.Edita(paginaModificada);
                return Ok(_mapper.Map<PaginaDto>(paginaModificada));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("Detalhes")]
        public async Task<ActionResult> Detalhes([FromQuery] int id)
        {
            try
            {
                Pagina pagina = await _paginaService.Detalhes(id);
                PaginaDto paginaDto = _mapper.Map<PaginaDto>(pagina);
                return Ok(paginaDto);
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
            List<Pagina> usuarios = await _paginaService.ListaTodos();
            List<PaginaDto> usuariosDto = _mapper.Map<List<PaginaDto>>(usuarios);
            return Ok(usuariosDto);
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete([FromQuery] int id)
        {
            try
            {
                return Ok(_paginaService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
