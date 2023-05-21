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
    public class IconeController : ControllerBase
    {
        private readonly AuthenticatedUser _user;
        public readonly IMapper _mapper;

        private readonly IIconeService _iconeService;

        public IconeController(
            IMapper mapper
         )
        {
            _user = new AuthenticatedUser(new HttpContextAccessor());
            _iconeService = new IconeService();
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Novo")]
        public async Task<ActionResult> Novo(IconeDto iconeDto)
        {
            try
            {
                Icone icone = _mapper.Map<Icone>(iconeDto);
                icone = await _iconeService.Novo(icone);
                return Ok(icone);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("Edita")]
        public async Task<ActionResult> Edita(IconeDto iconeDto)
        {
            try
            {
                Icone icone = await _iconeService.Detalhes(iconeDto.IdIcone);

                Icone iconeModificado = _mapper.Map<IconeDto, Icone>(iconeDto, icone);
                iconeModificado = await _iconeService.Edita(iconeModificado);
                return Ok(iconeModificado);
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
                Icone icone = await _iconeService.Detalhes(id);
                IconeDto iconeDto = _mapper.Map<IconeDto>(icone);
                return Ok(iconeDto);
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
            List<Icone> icones = await _iconeService.ListaTodos();
            List<IconeDto> iconesDto = _mapper.Map<List<IconeDto>>(icones);
            return Ok(iconesDto);
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete([FromQuery] int id)
        {
            try
            {
                return Ok(_iconeService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}