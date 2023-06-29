#nullable disable

using bahmapi.Dtos;
using bahmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace bahmapi.Services;


public interface ILogIconeZeroService
{

    public Task<LogIconeZero> Novo(LogIconeZero logPagina);
    public Task<LogIconeZero> Edita(LogIconeZero logPagina);
    public Task<LogIconeZero> Detalhes(int id);
    public Task<List<LogIconeZero>> ListaTodos();
    public int Delete(int id);
}

public partial class LogIconeZeroService : ILogIconeZeroService
{
    private readonly DatabaseContext _db;

    public LogIconeZeroService()
    {
        _db = new DatabaseContext();
    }

    public async Task<LogIconeZero> Novo(LogIconeZero logPagina)
    {
        _db.LogIconeZero.Add(logPagina);
        await _db.SaveChangesAsync();
        return logPagina;
    }


    public async Task<LogIconeZero> Edita(LogIconeZero logPagina)
    {
        _db.Entry(logPagina).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return logPagina;
    }

    public async Task<LogIconeZero> Detalhes(int id)
    {
        return await _db.LogIconeZero.Where(x => x.IdLogIconeZero == id).FirstOrDefaultAsync();
    }

    public async Task<List<LogIconeZero>> ListaTodos()
    {
        return await _db.LogIconeZero.Take(10).ToListAsync();

    }

    public int Delete(int id)
    {
        LogIconeZero logPagina = _db.LogIconeZero.Where(x => x.IdLogIconeZero == id).FirstOrDefault();
        _db.LogIconeZero.Remove(logPagina);
        return _db.SaveChanges();
    }

}