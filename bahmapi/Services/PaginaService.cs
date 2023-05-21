// #nullable disable

using bahmapi.Dtos;
using bahmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace bahmapi.Services;


public interface IPaginaService
{

    public Task<PaginaDto> Pagina(int PaginaId);
    public Task<Pagina> Novo(Pagina pagina);
    public Task<List<Pagina>> ListaTodos();
    public Task<Pagina> Detalhes(int id);
    public int Delete(int id);
    public Task<Pagina> Edita(Pagina pagina);
}

public partial class PaginaService : IPaginaService
{
    private readonly DatabaseContext _db;

    public PaginaService()
    {
        _db = new DatabaseContext();
    }

    public async Task<PaginaDto> Pagina(int PaginaId)
    {
        try
        {
            PaginaDto paginaDto = await _db.Pagina.Where(pagina => pagina.IdPagina == PaginaId)
                                                   .Join(_db.Concessionaria, pagina => pagina.ConcessionariaId,
                                                   concessionaria => concessionaria.IdConcessionaria,

                                                   (pagina, concessionaria) => new PaginaDto
                                                   {
                                                       IdPagina = pagina.IdPagina,
                                                       NomePagina = pagina.NomePagina,
                                                       EnderecoPagina = pagina.EnderecoPagina,
                                                       Concessionaria = new ConcessionariaDto
                                                       {
                                                           IdConcessionaria = concessionaria.IdConcessionaria,
                                                           NomeConcessionaria = concessionaria.NomeConcessionaria,
                                                           InfoConcessionaria = concessionaria.InfoConcessionaria
                                                       }
                                                   }).FirstOrDefaultAsync();



            paginaDto.ListSiteDto = await _db.PaginaSite.Where(x => x.PaginaId == PaginaId)
                                                    .Join(_db.Site,
                                                    paginaSite => paginaSite.SiteId,
                                                    site => site.IdSite,
                                                    (paginaSite, site) => new SiteDto()
                                                    {
                                                        IdSite=site.IdSite,
                                                        NomeSite=site.NomeSite,
                                                        LinkSite = site.LinkSite,
                                                        TipoSite = site.TipoSite
                                                    }).ToListAsync<SiteDto>();




            paginaDto.ListContatoDto = await _db.PaginaContato.Where(x => x.PaginaId == PaginaId)
                                                        .Join(_db.Contato,
                                                        paginaContato => paginaContato.ContatoId,
                                                        contato => contato.IdContato,
                                                        (paginaContato, contato) => new ContatoDto()
                                                        {
                                                            IdContato=contato.IdContato,
                                                            InfoContato = contato.InfoContato
                                                        })
                                                        .ToListAsync<ContatoDto>();


            return paginaDto;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Pagina> Novo(Pagina pagina)
    {
        _db.Pagina.Add(pagina);
        await _db.SaveChangesAsync();
        return pagina;
    }


    public async Task<Pagina> Edita(Pagina pagina)
    {
        var paginaContato = await _db.PaginaContato.Where(x=>x.PaginaId==pagina.IdPagina).ToListAsync();

        var paginaSite = await _db.PaginaSite.Where(x=>x.PaginaId==pagina.IdPagina).ToListAsync();

        _db.PaginaContato.RemoveRange(paginaContato);

        _db.PaginaSite.RemoveRange(paginaSite);
        _db.Entry(pagina).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return pagina;
    }

    public async Task<Pagina> Detalhes(int id)
    {
        return await _db.Pagina.Where(x => x.IdPagina == id).FirstOrDefaultAsync();
    }

    public int Delete(int id)
    {
        Pagina pagina = _db.Pagina.Where(x => x.IdPagina == id).FirstOrDefault();
        _db.Pagina.Remove(pagina);
        return _db.SaveChanges();
    }

    public async Task<List<Pagina>> ListaTodos()
    {
        return await _db.Pagina.Join(_db.Concessionaria, pagina => pagina.ConcessionariaId,
                                           concessionaria => concessionaria.IdConcessionaria,
                                           (pagina, concessionaria) => new Pagina
                                           {
                                               IdPagina = pagina.IdPagina,
                                               NomePagina = pagina.NomePagina,
                                               EnderecoPagina = pagina.EnderecoPagina,
                                               Concessionaria = new Concessionaria
                                               {
                                                   NomeConcessionaria = concessionaria.NomeConcessionaria,
                                                   InfoConcessionaria = concessionaria.InfoConcessionaria
                                               }
                                           }).OrderByDescending(x => x.IdPagina).ToListAsync();
    }
}