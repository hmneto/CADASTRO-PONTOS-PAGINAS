// #nullable disable

using bahmapi.Dtos;
using bahmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace bahmapi.Services;


public interface ILogMapaService
{

    public Task<LogMapa> Novo(LogMapa logMapa);
    public Task<LogMapa> Edita(LogMapa logMapa);
    public Task<LogMapa> Detalhes(int id);
    public Task<List<LogMapa>> ListaTodos();
    public int Delete(int id);
    // public void Registra(AuthenticatedUser _user, Ponto Ponto, int Zoom, List<PontoDto> ListaPontos);
}

public partial class LogMapaService : ILogMapaService
{
    private readonly DatabaseContext _db;

    public LogMapaService()
    {
        _db = new DatabaseContext();
    }

    public async Task<LogMapa> Novo(LogMapa logMapa)
    {
        _db.LogMapa.Add(logMapa);
        await _db.SaveChangesAsync();
        return logMapa;
    }


    public async Task<LogMapa> Edita(LogMapa logMapa)
    {
        _db.Entry(logMapa).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return logMapa;
    }

    public async Task<LogMapa> Detalhes(int id)
    {
        return await _db.LogMapa.Where(x => x.IdLogMapa == id).FirstOrDefaultAsync();
    }

    public async Task<List<LogMapa>> ListaTodos()
    {
        return await _db.LogMapa.Take(10).ToListAsync();

    }

    public int Delete(int id)
    {
        LogMapa logMapa = _db.LogMapa.Where(x => x.IdLogMapa == id).FirstOrDefault();
        _db.LogMapa.Remove(logMapa);
        return _db.SaveChanges();
    }

    // public void Registra(AuthenticatedUser _user, Ponto Ponto, int Zoom, List<PontoDto> ListaPontos)
    // {


    //     int zoom = 0;
    //     if (Convert.ToInt32(Zoom) < 10)
    //     {
    //         zoom = Convert.ToInt32(Zoom) / 2;

    //     }
    //     else
    //     {
    //         zoom = Convert.ToInt32(Zoom) / 11;

    //     }

    //     int latMin = Convert.ToInt32(Ponto.LatitudePonto) - zoom;

    //     int latMax = Convert.ToInt32(Ponto.LatitudePonto) + zoom;

    //     int longMin = Convert.ToInt32(Ponto.LongitudePonto) - zoom;

    //     int longMax = Convert.ToInt32(Ponto.LongitudePonto) + zoom;
    //     // var a = _user.Email;
    //     // UsuarioDto usuario = (from x in _db.Usuario
    //     //                       where x.EmailUsuario == _user.Email
    //     //                       select new UsuarioDto { IdUsuario = x.IdUsuario, ClienteId = x.ClienteId })
    //     //                .FirstOrDefault();

    //     LogMapa logsPontos = new LogMapa
    //     {
    //         DataHora = DateTime.Now,
    //         LatLong = $"{latMin} {latMax} {longMin} {longMax}",
    //         QtdPontos = ListaPontos.Count,
    //         UsuarioId = _user.Id
    //     };

    //     _db.Add(logsPontos);
    //     _db.SaveChanges();
    // }

}