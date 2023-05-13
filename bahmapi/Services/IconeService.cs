#nullable disable

using bahmapi.Dtos;
using bahmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace bahmapi.Services;


public interface IIconeService
{

    public Task<Icone> Novo(Icone icone);
    public Task<Icone> Edita(Icone icone);
    public Task<Icone> Detalhes(int id);
    public Task<List<Icone>> ListaTodos();
    public int Delete(int id);

}

public partial class IconeService : IIconeService
{
    private readonly DatabaseContext _db;

    public IconeService()
    {
        _db = new DatabaseContext();
    }

    public async Task<Icone> Novo(Icone icone)
    {
        _db.Icone.Add(icone);
        await _db.SaveChangesAsync();
        return icone;
    }


    public async Task<Icone> Edita(Icone icone)
    {
        _db.Entry(icone).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return icone;
    }

    public async Task<Icone> Detalhes(int id)
    {
        return await _db.Icone.Where(x => x.IdIcone == id).FirstOrDefaultAsync();
    }

    public async Task<List<Icone>> ListaTodos()
    {
        return await _db.Icone.OrderByDescending(x=>x.IdIcone).ToListAsync();

    }

    public int Delete(int id)
    {
        Icone icone = _db.Icone.Where(x => x.IdIcone == id).FirstOrDefault();
        _db.Icone.Remove(icone);
        return _db.SaveChanges();
    }

}