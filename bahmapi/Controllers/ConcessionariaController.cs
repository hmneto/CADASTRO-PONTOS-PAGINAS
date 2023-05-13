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
    [Authorize(Roles = "admin")]
    public class ConcessionariaController : ControllerBase
    {
        private readonly AuthenticatedUser _user;
        public readonly IMapper _mapper;
        private readonly IConcessionariaService _concessionariaService;
        public ConcessionariaController(
            IMapper mapper,
            AuthenticatedUser user,
            IConcessionariaService concessionariaService
         )
        {
            _user = user;
            _concessionariaService = concessionariaService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Novo")]
        public async Task<ActionResult> Novo(ConcessionariaDto concessionariaDto)
        {
            try
            {
                Concessionaria concessionaria = _mapper.Map<Concessionaria>(concessionariaDto);
                concessionaria = await _concessionariaService.Novo(concessionaria);
                return Ok(concessionaria);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("Edita")]
        public async Task<ActionResult> Edita(ConcessionariaDto concessionariaDto)
        {
            try
            {
                Concessionaria concessionaria = await _concessionariaService.Detalhes(concessionariaDto.IdConcessionaria);

                Concessionaria concessionariaModificada = _mapper.Map<ConcessionariaDto, Concessionaria>(concessionariaDto, concessionaria);
                concessionariaModificada = await _concessionariaService.Edita(concessionariaModificada);
                return Ok(concessionariaModificada);
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
                Concessionaria concessionaria = await _concessionariaService.Detalhes(id);
                ConcessionariaDto concessionariaDto = _mapper.Map<ConcessionariaDto>(concessionaria);
                return Ok(concessionariaDto);
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
            List<Concessionaria> concessionarias = await _concessionariaService.ListaTodos();
            List<ConcessionariaDto> concessionariasDto = _mapper.Map<List<ConcessionariaDto>>(concessionarias);
            return Ok(concessionariasDto);
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete([FromQuery] int id)
        {
            try
            {
                return Ok(_concessionariaService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}