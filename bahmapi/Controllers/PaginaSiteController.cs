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
    public class PaginaSiteController : ControllerBase
    {
        private readonly AuthenticatedUser _user;
        public readonly IMapper _mapper;

        private readonly IPaginaSiteService _paginaSiteService;

        public PaginaSiteController(
            IMapper mapper
         )
        {
            _user = new AuthenticatedUser(new HttpContextAccessor());
            _paginaSiteService = new PaginaSiteService();
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Novo")]
        public async Task<ActionResult> Novo(PaginaSiteDto paginaSiteDto)
        {
            try
            {
                PaginaSite paginaSite = _mapper.Map<PaginaSite>(paginaSiteDto);
                paginaSite = await _paginaSiteService.Novo(paginaSite);
                return Ok(paginaSite);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("Edita")]
        public async Task<ActionResult> Edita(PaginaSiteDto paginaSiteDto)
        {
            try
            {
                PaginaSite paginaSite = await _paginaSiteService.Detalhes(paginaSiteDto.IdPaginaSite);

                PaginaSite paginaSiteModificado = _mapper.Map<PaginaSiteDto, PaginaSite>(paginaSiteDto, paginaSite);
                paginaSiteModificado = await _paginaSiteService.Edita(paginaSiteModificado);
                return Ok(paginaSiteModificado);
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
                PaginaSite paginaSite = await _paginaSiteService.Detalhes(id);
                PaginaSiteDto paginaSiteDto = _mapper.Map<PaginaSiteDto>(paginaSite);
                return Ok(paginaSiteDto);
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
            List<PaginaSite> paginaSites = await _paginaSiteService.ListaTodos();
            List<PaginaSiteDto> paginaSitesDto = _mapper.Map<List<PaginaSiteDto>>(paginaSites);
            return Ok(paginaSitesDto);
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete([FromQuery] int id)
        {
            try
            {
                return Ok(_paginaSiteService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("AlteraOrdemUp")]
        public ActionResult AlteraOrdemUp([FromQuery] int paginaId, int IdPaginaContato)
        {
            try
            {
                _paginaSiteService.AlteraOrdemUp(paginaId, IdPaginaContato);
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
                _paginaSiteService.AlteraOrdemUpDown(paginaId, IdPaginaContato);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}