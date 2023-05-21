// #nullable disable

using bahmapi.Dtos;
using bahmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace bahmapi.Services;


public interface IContatoService
{

    public Task<Contato> Novo(Contato contato);
    public Task<Contato> Edita(Contato contato);
    public Task<Contato> Detalhes(int id);
    public Task<List<Contato>> ListaTodos();
    public int Delete(int id);
}

public partial class ContatoService : IContatoService
{
    private readonly DatabaseContext _db;

    public ContatoService()
    {
        _db = new DatabaseContext();
    }

    public async Task<Contato> Novo(Contato contato)
    {
        _db.Contato.Add(contato);
        await _db.SaveChangesAsync();
        return contato;
    }


    public async Task<Contato> Edita(Contato contato)
    {
        _db.Entry(contato).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return contato;
    }

    public async Task<Contato> Detalhes(int id)
    {
        return await _db.Contato.Where(x => x.IdContato== id).FirstOrDefaultAsync();
    }

    public async Task<List<Contato>> ListaTodos()
    {
        return await _db.Contato.OrderByDescending(x=>x.IdContato).ToListAsync();
    }

    public int Delete(int id)
    {
        Contato cliente = _db.Contato.Where(x => x.IdContato == id).FirstOrDefault();
        _db.Contato.Remove(cliente);
        return _db.SaveChanges();
    }
}