// #nullable disable

using bahmapi.Dtos;
using bahmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace bahmapi.Services;


public interface IPaginaSiteService
{

    public Task<PaginaSite> Novo(PaginaSite paginaSite);
    public Task<PaginaSite> Edita(PaginaSite paginaSite);
    public Task<PaginaSite> Detalhes(int id);
    public Task<List<PaginaSite>> ListaTodos();
    public int Delete(int id);
    public void AlteraOrdemUp(int paginaId, int IdPaginaSite);
    public void AlteraOrdemUpDown(int paginaId, int IdPaginaSite);

}

public partial class PaginaSiteService : IPaginaSiteService
{
    private readonly DatabaseContext _db;

    public PaginaSiteService()
    {
        _db = new DatabaseContext();
    }

    public async Task<PaginaSite> Novo(PaginaSite paginaSite)
    {
        _db.PaginaSite.Add(paginaSite);
        await _db.SaveChangesAsync();
        return paginaSite;
    }


    public async Task<PaginaSite> Edita(PaginaSite paginaSite)
    {
        _db.Entry(paginaSite).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return paginaSite;
    }

    public async Task<PaginaSite> Detalhes(int id)
    {
        return await _db.PaginaSite.Where(x => x.IdPaginaSite == id).FirstOrDefaultAsync();
    }

    public async Task<List<PaginaSite>> ListaTodos()
    {
        return await _db.PaginaSite.Take(10).ToListAsync();

    }

    public int Delete(int id)
    {
        PaginaSite paginaSite = _db.PaginaSite.Where(x => x.IdPaginaSite == id).FirstOrDefault();
        _db.PaginaSite.Remove(paginaSite);
        return _db.SaveChanges();
    }

    public async void AlteraOrdemUp(int paginaId, int IdPaginaSite)
    {

        List<PaginaSite> paginaSites = _db.PaginaSite.Where(x => x.PaginaId == paginaId).ToList();
        int paginaSiteIndex = paginaSites.FindIndex(x => x.IdPaginaSite == IdPaginaSite);
        int aux = paginaSites[paginaSiteIndex - 1].SiteId;
        paginaSites[paginaSiteIndex - 1].SiteId = paginaSites[paginaSiteIndex].SiteId;
        paginaSites[paginaSiteIndex].SiteId = aux;
        await _db.SaveChangesAsync();
    }

    public async void AlteraOrdemUpDown(int paginaId, int IdPaginaSite)
    {
        List<PaginaSite> paginaSites = _db.PaginaSite.Where(x => x.PaginaId == paginaId).ToList();
        var paginaSiteIndex = paginaSites.FindIndex(x => x.IdPaginaSite == IdPaginaSite);
        int aux = paginaSites[paginaSiteIndex + 1].SiteId;
        paginaSites[paginaSiteIndex + 1].SiteId = paginaSites[paginaSiteIndex].SiteId;
        paginaSites[paginaSiteIndex].SiteId = aux;
        await _db.SaveChangesAsync();
    }
}