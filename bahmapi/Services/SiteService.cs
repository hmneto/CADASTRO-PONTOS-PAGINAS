// #nullable disable

using bahmapi.Dtos;
using bahmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace bahmapi.Services;


public interface ISiteService
{

    public Task<Site> Novo(Site site);
    public Task<Site> Edita(Site site);
    public Task<Site> Detalhes(int id);
    public Task<List<Site>> ListaTodos();
    public int Delete(int id);

}

public partial class SiteService : ISiteService
{
    private readonly DatabaseContext _db;

    public SiteService()
    {
        _db = new DatabaseContext();
    }

    public async Task<Site> Novo(Site site)
    {
        _db.Site.Add(site);
        await _db.SaveChangesAsync();
        return site;
    }


    public async Task<Site> Edita(Site site)
    {
        _db.Entry(site).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return site;
    }

    public async Task<Site> Detalhes(int id)
    {
        return await _db.Site.Where(x => x.IdSite == id).FirstOrDefaultAsync();
    }

    public async Task<List<Site>> ListaTodos()
    {
        return await _db.Site.OrderByDescending(x=>x.IdSite).ToListAsync();

    }

    public int Delete(int id)
    {
        Site site = _db.Site.Where(x => x.IdSite == id).FirstOrDefault();
        _db.Site.Remove(site);
        return _db.SaveChanges();
    }

}