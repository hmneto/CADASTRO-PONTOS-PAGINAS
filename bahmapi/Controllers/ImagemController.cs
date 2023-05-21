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
using System.Text;

namespace bahmapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ImagemController : ControllerBase
    {
        private readonly AuthenticatedUser _user;
        public readonly IMapper _mapper;

        private readonly IImagemService _imagemService;

        public ImagemController(
            IMapper mapper
         )
        {
            _user = new AuthenticatedUser(new HttpContextAccessor());
            _imagemService = new ImagemService();
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Imagem")]
        public ActionResult GetImagem(string i)
        {
            try
            {
                DatabaseContext bahmDbContext = new DatabaseContext();
                string retorno = "";
                Imagem imagem = bahmDbContext.Imagem.Where(x => x.NomeImagem == i).FirstOrDefault();
                if (imagem.ExtensaoImagem == ".jpg")
                    retorno = "image/jpg";

                if (imagem.ExtensaoImagem == ".png")
                    retorno = "image/png";

                if (imagem.ExtensaoImagem == ".gif")
                    retorno = "image/gif";

                return File(imagem.BinarioImagem, retorno);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("Novo")]
        public async Task<ActionResult> Novo(ImagemDto imagemDto)
        {
            try
            {
                if (imagemDto.FotoString != null)
                {
                    String[] substrings = imagemDto.FotoString.Split(',');
                    string header = substrings[0];
                    string imagem2 = substrings[1];
                    imagemDto.BinarioImagem = Convert.FromBase64String(imagem2);
                }

                Imagem imagem = _mapper.Map<Imagem>(imagemDto);
                imagem = await _imagemService.Novo(imagem);
                return Ok(imagem);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("Edita")]
        public async Task<ActionResult> Edita(ImagemDto imagemDto)
        {
            try
            {
                Imagem imagem = await _imagemService.Detalhes(imagemDto.IdImagem);

                if (imagemDto.FotoString != null)
                {
                    String[] substrings = imagemDto.FotoString.Split(',');
                    string header = substrings[0];
                    string imagem2 = substrings[1];
                    imagemDto.BinarioImagem = Convert.FromBase64String(imagem2);
                }
                else
                    imagemDto.BinarioImagem = imagem.BinarioImagem;

                Imagem imagemModificado = _mapper.Map<ImagemDto, Imagem>(imagemDto, imagem);
                imagemModificado = await _imagemService.Edita(imagemModificado);
                return Ok(imagemModificado);
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
                Imagem imagem = await _imagemService.Detalhes(id);
                ImagemDto imagemDto = _mapper.Map<ImagemDto>(imagem);
                return Ok(imagemDto);
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
            List<Imagem> imagems = await _imagemService.ListaTodos();
            List<ImagemDto> imagemsDto = _mapper.Map<List<ImagemDto>>(imagems);
            return Ok(imagemsDto);
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete([FromQuery] int id)
        {
            try
            {
                return Ok(_imagemService.Delete(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}