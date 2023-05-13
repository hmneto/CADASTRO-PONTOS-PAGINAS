#nullable disable

using bahmapi.Dtos;
using bahmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace bahmapi.Services;


public interface IConcessionariaService
{

    public Task<Concessionaria> Novo(Concessionaria concessionaria);
    public Task<Concessionaria> Edita(Concessionaria concessionaria);
    public Task<Concessionaria> Detalhes(int id);
    public Task<List<Concessionaria>> ListaTodos();
    public Task<int> Delete(int id);

}

public partial class ConcessionariaService : IConcessionariaService
{
    private readonly DatabaseContext _db;

    public ConcessionariaService(DatabaseContext db)
    {
        _db = db;
    }

    public async Task<Concessionaria> Novo(Concessionaria concessionaria)
    {
        _db.Concessionaria.Add(concessionaria);
        await _db.SaveChangesAsync();
        return concessionaria;
    }


    public async Task<Concessionaria> Edita(Concessionaria concessionaria)
    {
        _db.Entry(concessionaria).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return concessionaria;
    }

    public async Task<Concessionaria> Detalhes(int id)
    {
        return await _db.Concessionaria.Where(x => x.IdConcessionaria == id).FirstOrDefaultAsync();
    }

    public async Task<List<Concessionaria>> ListaTodos()
    {
        return await _db.Concessionaria.OrderByDescending(x=>x.IdConcessionaria).ToListAsync();

    }

    public async Task<int> Delete(int id)
    {
        Concessionaria concessionaria = await _db.Concessionaria.Where(x => x.IdConcessionaria == id).FirstOrDefaultAsync();
        _db.Concessionaria.Remove(concessionaria);
        return _db.SaveChanges();
    }

}