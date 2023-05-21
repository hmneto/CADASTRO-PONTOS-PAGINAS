// #nullable disable

using bahmapi.Dtos;
using bahmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace bahmapi.Services;


public interface IPontoService
{

    public Task<List<PontoDto>> Ponto(Ponto Ponto, int Zoom);
    public Task<Ponto> Novo(Ponto ponto);
    public Task<Ponto> Edita(Ponto pagina);
    public Task<Ponto> Detalhes(int id);
    public Task<List<Ponto>> ListaTodos();
    public int Delete(int id);

}

public partial class PontoService : IPontoService
{
    private readonly DatabaseContext _db;

    public PontoService()
    {
        _db = new DatabaseContext();
    }

    public async Task<List<PontoDto>> Ponto(Ponto Ponto, int Zoom)
    {

        int zoom = 0;
        if (Convert.ToInt32(Zoom) < 10)
        {
            zoom = Convert.ToInt32(Zoom) / 2;

        }
        else
        {
            zoom = Convert.ToInt32(Zoom) / 11;

        }

        int latMin = Convert.ToInt32(Ponto.LatitudePonto) - zoom;

        int latMax = Convert.ToInt32(Ponto.LatitudePonto) + zoom;

        int longMin = Convert.ToInt32(Ponto.LongitudePonto) - zoom;

        int longMax = Convert.ToInt32(Ponto.LongitudePonto) + zoom;


        List<PontoDto> ListaPontos = await (from pontoDb in _db.Ponto
                                            join iconeDb in _db.Icone
                                            on pontoDb.IconeId equals iconeDb.IdIcone
                                            where pontoDb.LatitudePonto > latMin &&
                                            pontoDb.LatitudePonto < latMax &&
                                            pontoDb.LongitudePonto > longMin &&
                                            pontoDb.LongitudePonto < longMax
                                            //   && pontoDb.PaginaId != 0
                                            select new PontoDto
                                            {
                                                IdPonto = pontoDb.IdPonto,
                                                //  IdPonto = JsonWebToken.Encode(new { t = pontoDb.IdPonto },Secret.SecretString,JwtHashAlgorithm.RS256),
                                                PaginaId = pontoDb.PaginaId,
                                                //  pagina = JsonWebToken.Encode(new { t = Convert.ToString(pontoDb.PaginaId) }, Secret.SecretString,JwtHashAlgorithm.RS256),
                                                NomePonto = pontoDb.NomePonto,
                                                Icone = new IconeDto { LinkIcone = iconeDb.LinkIcone, AcaoIcone = iconeDb.AcaoIcone },
                                                LatitudePonto = pontoDb.LatitudePonto,
                                                LongitudePonto = pontoDb.LongitudePonto
                                            }).ToListAsync();


        return ListaPontos;

    }

    public async Task<Ponto> Novo(Ponto ponto)
    {
        //ponto.Pagina = _db.Pagina.Where(x=>x.IdPagina == ponto.PaginaId).FirstOrDefault();
        //ponto.Icone = _db.Icone.Where(x=>x.IdIcone == ponto.IconeId).FirstOrDefault();
        ponto.ObservacaoPonto = "";

        _db.Ponto.Add(ponto);
        await _db.SaveChangesAsync();
        return ponto;
    }


    public async Task<Ponto> Edita(Ponto ponto)
    {
        ponto.Pagina = _db.Pagina.Where(x=>x.IdPagina == ponto.PaginaId).FirstOrDefault();
        _db.Entry(ponto).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return ponto;
    }

    public async Task<Ponto> Detalhes(int id)
    {
        return await _db.Ponto
        .Where(x => x.IdPonto == id)
        .Join(_db.Icone, ponto => ponto.IconeId,
                                    icone => icone.IdIcone,
                                    (ponto, icone) => new Ponto
                                    {
                                        Icone = new Icone
                                        {
                                            IdIcone = icone.IdIcone,
                                            NomeIcone = icone.NomeIcone
                                        },
                                        IdPonto = ponto.IdPonto,
                                        LatitudePonto = ponto.LatitudePonto,
                                        LongitudePonto = ponto.LongitudePonto,
                                        NomePonto = ponto.NomePonto,
                                        Pagina = _db.Pagina
                                        .Where(x => x.IdPagina == ponto.PaginaId)
                                        .FirstOrDefault()
                                    }).FirstOrDefaultAsync();

    }

    public async Task<List<Ponto>> ListaTodos()
    {
        return await _db.Ponto.Take(10).ToListAsync();
    }

    public int Delete(int id)
    {
        Ponto ponto = _db.Ponto.Where(x => x.IdPonto == id).FirstOrDefault();
        _db.Ponto.Remove(ponto);
        return _db.SaveChanges();
    }

}