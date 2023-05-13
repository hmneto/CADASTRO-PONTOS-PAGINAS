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
    public class ContatoController : ControllerBase
    {
        private readonly AuthenticatedUser _user;
        public readonly IMapper _mapper;

        private readonly IContatoService _contatoService;
        public ContatoController(
            IMapper mapper
         )
        {
            _user = new AuthenticatedUser(new HttpContextAccessor());
            _contatoService = new ContatoService();
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Novo")]
        public async Task<ActionResult> Novo(ContatoDto contatoDto)
        {
            try
            {
                Contato contato = _mapper.Map<Contato>(contatoDto);
                contato = await _contatoService.Novo(contato);
                return Ok(contato);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("Edita")]
        public async Task<ActionResult> Edita(ContatoDto contatoDto)
        {
            try
            {
                Contato contato = await _contatoService.Detalhes(contatoDto.IdContato);

                Contato contatoModificado = _mapper.Map<ContatoDto, Contato>(contatoDto, contato);
                contatoModificado = await _contatoService.Edita(contatoModificado);
                return Ok(contatoModificado);
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
                Contato contato = await _contatoService.Detalhes(id);
                ContatoDto contatoDto = _mapper.Map<ContatoDto>(contato);
                return Ok(contatoDto);
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
            List<Contato> contatos = await _contatoService.ListaTodos();
            List<ContatoDto> contatosDto = _mapper.Map<List<ContatoDto>>(contatos);
            return Ok(contatosDto);
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete([FromQuery] int id)
        {
            try
            {
                return Ok(_contatoService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}