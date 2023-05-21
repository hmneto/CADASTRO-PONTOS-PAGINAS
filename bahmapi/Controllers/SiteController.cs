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
    public class SiteController : ControllerBase
    {
        private readonly AuthenticatedUser _user;
        public readonly IMapper _mapper;

        private readonly ISiteService _siteService;

        public SiteController(
            IMapper mapper
         )
        {
            _user = new AuthenticatedUser(new HttpContextAccessor());
            _siteService = new SiteService();
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Novo")]
        public async Task<ActionResult> Novo(SiteDto siteDto)
        {
            try
            {
                string[] tiposSite = new string[]
                {"STREET","FOTO","WIKIMAPIA_SAT","WIKIMAPIA_FRIO","PM","SITE","ABCR","CONCESSIONARIA","FOTO_MAPA","WIKIPEDIA","LEI"};
                if (!tiposSite.Contains(siteDto.TipoSite)) throw new Exception("Não valido");
                siteDto.SiteUsuarioId = _user.Id;

                Site site = _mapper.Map<Site>(siteDto);
                site = await _siteService.Novo(site);
                return Ok(site);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("Edita")]
        public async Task<ActionResult> Edita(SiteDto siteDto)
        {
            try
            {
                string[] tiposSite = new string[]
                {"STREET","FOTO","WIKIMAPIA_SAT","WIKIMAPIA_FRIO","PM","SITE","ABCR","CONCESSIONARIA","FOTO_MAPA","WIKIPEDIA","LEI"};
                if (!tiposSite.Contains(siteDto.TipoSite)) throw new Exception("Não valido");
                siteDto.SiteUsuarioId = _user.Id;
                Site site = await _siteService.Detalhes(siteDto.IdSite);

                Site siteModificado = _mapper.Map<SiteDto, Site>(siteDto, site);
                siteModificado = await _siteService.Edita(siteModificado);
                return Ok(siteModificado);
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
                Site site = await _siteService.Detalhes(id);
                SiteDto siteDto = _mapper.Map<SiteDto>(site);
                return Ok(siteDto);
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
            List<Site> sites = await _siteService.ListaTodos();
            List<SiteDto> sitesDto = _mapper.Map<List<SiteDto>>(sites);
            return Ok(sitesDto);
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete([FromQuery] int id)
        {
            try
            {
                return Ok(_siteService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}