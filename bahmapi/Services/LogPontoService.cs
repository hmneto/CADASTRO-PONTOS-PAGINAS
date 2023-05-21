// #nullable disable

using bahmapi.Dtos;
using bahmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace bahmapi.Services;


public interface ILogPontoService
{

    public Task<LogPonto> Novo(LogPonto logPonto);
    public Task<LogPonto> Edita(LogPonto logPonto);
    public Task<LogPonto> Detalhes(int id);
    public Task<List<LogPonto>> ListaTodos();
    public int Delete(int id);
    public void Registra(AuthenticatedUser _user, Ponto Ponto, int Zoom, List<PontoDto> ListaPontos);
}

public partial class LogPontoService : ILogPontoService
{
    private readonly DatabaseContext _db;

    public LogPontoService()
    {
        _db = new DatabaseContext();
    }

    public async Task<LogPonto> Novo(LogPonto logPonto)
    {
        _db.LogPonto.Add(logPonto);
        await _db.SaveChangesAsync();
        return logPonto;
    }


    public async Task<LogPonto> Edita(LogPonto logPonto)
    {
        _db.Entry(logPonto).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return logPonto;
    }

    public async Task<LogPonto> Detalhes(int id)
    {
        return await _db.LogPonto.Where(x => x.IdLogPonto == id).FirstOrDefaultAsync();
    }

    public async Task<List<LogPonto>> ListaTodos()
    {
        return await _db.LogPonto.Take(10).ToListAsync();

    }

    public int Delete(int id)
    {
        LogPonto logPonto = _db.LogPonto.Where(x => x.IdLogPonto == id).FirstOrDefault();
        _db.LogPonto.Remove(logPonto);
        return _db.SaveChanges();
    }

    public void Registra(AuthenticatedUser _user, Ponto Ponto, int Zoom, List<PontoDto> ListaPontos)
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
        // var a = _user.Email;
        // UsuarioDto usuario = (from x in _db.Usuario
        //                       where x.EmailUsuario == _user.Email
        //                       select new UsuarioDto { IdUsuario = x.IdUsuario, ClienteId = x.ClienteId })
        //                .FirstOrDefault();

        LogPonto logsPontos = new LogPonto
        {
            DataHora = DateTime.Now,
            LatLong = $"{latMin} {latMax} {longMin} {longMax}",
            QtdPontos = ListaPontos.Count,
            UsuarioId = _user.Id
        };

        _db.Add(logsPontos);
        _db.SaveChanges();
    }

}