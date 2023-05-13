#nullable disable

using bahmapi.Dtos;
using bahmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace bahmapi.Services;


public interface ILogPaginaService
{

    public Task<LogPagina> Novo(LogPagina logPagina);
    public Task<LogPagina> Edita(LogPagina logPagina);
    public Task<LogPagina> Detalhes(int id);
    public Task<List<LogPagina>> ListaTodos();
    public int Delete(int id);
    public void Registra(AuthenticatedUser _user, int PaginaId);

}

public partial class LogPaginaService : ILogPaginaService
{
    private readonly DatabaseContext _db;

    public LogPaginaService()
    {
        _db = new DatabaseContext();
    }

    public async Task<LogPagina> Novo(LogPagina logPagina)
    {
        _db.LogPagina.Add(logPagina);
        await _db.SaveChangesAsync();
        return logPagina;
    }


    public async Task<LogPagina> Edita(LogPagina logPagina)
    {
        _db.Entry(logPagina).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return logPagina;
    }

    public async Task<LogPagina> Detalhes(int id)
    {
        return await _db.LogPagina.Where(x => x.IdLogPagina == id).FirstOrDefaultAsync();
    }

    public async Task<List<LogPagina>> ListaTodos()
    {
        return await _db.LogPagina.Take(10).ToListAsync();

    }

    public int Delete(int id)
    {
        LogPagina logPagina = _db.LogPagina.Where(x => x.IdLogPagina == id).FirstOrDefault();
        _db.LogPagina.Remove(logPagina);
        return _db.SaveChanges();
    }

    public void Registra(AuthenticatedUser _user, int PaginaId)
    {

        LogPagina log = new LogPagina
        {
            PaginaId = PaginaId,
            UsuarioId = _user.Id
        };

        _db.LogPagina.Add(log);
        _db.SaveChanges();

    }

}