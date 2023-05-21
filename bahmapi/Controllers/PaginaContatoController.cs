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
using Microsoft.EntityFrameworkCore;

namespace bahmapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PaginaContatoController : ControllerBase
    {
        private readonly AuthenticatedUser _user;
        public readonly IMapper _mapper;

        private readonly IPaginaContatoService _paginaContatoService;

        public PaginaContatoController(
            IMapper mapper
         )
        {
            _user = new AuthenticatedUser(new HttpContextAccessor());
            _paginaContatoService = new PaginaContatoService();
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Novo")]
        public async Task<ActionResult> Novo(PaginaContatoDto paginaContatoDto)
        {
            try
            {
                PaginaContato paginaContato = _mapper.Map<PaginaContato>(paginaContatoDto);
                paginaContato = await _paginaContatoService.Novo(paginaContato);
                return Ok(paginaContato);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("Edita")]
        public async Task<ActionResult> Edita(PaginaContatoDto paginaContatoDto)
        {
            try
            {
                PaginaContato paginaContato = await _paginaContatoService.Detalhes(paginaContatoDto.IdPaginaContato);

                PaginaContato paginaContatoModificado = _mapper.Map<PaginaContatoDto, PaginaContato>(paginaContatoDto, paginaContato);
                paginaContatoModificado = await _paginaContatoService.Edita(paginaContatoModificado);
                return Ok(paginaContatoModificado);
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
                PaginaContato paginaContato = await _paginaContatoService.Detalhes(id);
                PaginaContatoDto paginaContatoDto = _mapper.Map<PaginaContatoDto>(paginaContato);
                return Ok(paginaContatoDto);
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
            List<PaginaContato> paginaContatos = await _paginaContatoService.ListaTodos();
            List<PaginaContatoDto> paginaContatosDto = _mapper.Map<List<PaginaContatoDto>>(paginaContatos);
            return Ok(paginaContatosDto);
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete([FromQuery] int id)
        {
            try
            {
                return Ok(_paginaContatoService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet]
        [Route("ListaTodosContatos")]
        public async Task<ActionResult> ListaTodosPagina(int id)
        {
            List<PaginaContato> paginaContatos = await _paginaContatoService.ListaTodosContatos(id);
            List<PaginaContatoDto> paginaContatosDto = _mapper.Map<List<PaginaContatoDto>>(paginaContatos);
            return Ok(paginaContatosDto);
        }


        [HttpGet]
        [Route("AlteraOrdemUp")]
        public ActionResult AlteraOrdemUp([FromQuery] int paginaId, int IdPaginaContato)
        {
            try
            {
                _paginaContatoService.AlteraOrdemUp(paginaId, IdPaginaContato);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("AlteraOrdemUpDown")]
        public ActionResult AlteraOrdemUpDown([FromQuery] int paginaId, int IdPaginaContato)
        {
            try
            {
                _paginaContatoService.AlteraOrdemUpDown(paginaId, IdPaginaContato);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}