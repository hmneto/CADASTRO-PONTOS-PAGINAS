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
    [Authorize(Roles = "admin")]
    public class ClienteController : ControllerBase
    {
        private readonly AuthenticatedUser _user;
        public readonly IMapper _mapper;
        private readonly IClienteService _clienteService;

        public ClienteController(
            IMapper mapper,
            IClienteService clienteService,
            AuthenticatedUser user

         )
        {
            _user = user;
            _clienteService = clienteService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Novo")]
        public async Task<ActionResult> Novo(ClienteDto clienteDto)
        {
            try
            {
                Cliente cliente = _mapper.Map<Cliente>(clienteDto);
                cliente = await _clienteService.Novo(cliente);
                return Ok(cliente);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("Edita")]
        public async Task<ActionResult> Edita(ClienteDto clienteDto)
        {
            try
            {
                Cliente cliente = await _clienteService.Detalhes(clienteDto.IdCliente);
                if(cliente == null) return NotFound("Cliente não encontrado!");
                Cliente clienteModificado = _mapper.Map<ClienteDto, Cliente>(clienteDto, cliente);
                clienteModificado = await _clienteService.Edita(clienteModificado);
                return Ok(clienteModificado);
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
                Cliente cliente = await _clienteService.Detalhes(id);
                if(cliente == null) return NotFound("Cliente não encontrado!");
                ClienteDto clienteDto = _mapper.Map<ClienteDto>(cliente);
                return Ok(clienteDto);
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
            List<Cliente> clientes = await _clienteService.ListaTodos();
            List<ClienteDto> clientesDto = _mapper.Map<List<ClienteDto>>(clientes);
            return Ok(clientesDto);
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete([FromQuery] int id)
        {
            try
            {
                return Ok(_clienteService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}