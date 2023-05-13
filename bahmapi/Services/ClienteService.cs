#nullable disable

using bahmapi.Dtos;
using bahmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace bahmapi.Services;


public interface IClienteService
{

    public Task<Cliente> Novo(Cliente cliente);
    public Task<Cliente> Edita(Cliente cliente);
    public Task<Cliente> Detalhes(int id);
    public Task<List<Cliente>> ListaTodos();
    public int Delete(int id);

}

public partial class ClienteService : IClienteService
{
    private readonly DatabaseContext _db;

    public ClienteService(DatabaseContext db)
    {
        _db = db;
    }

    public async Task<Cliente> Novo(Cliente cliente)
    {
        _db.Cliente.Add(cliente);
        await _db.SaveChangesAsync();
        return cliente;
    }


    public async Task<Cliente> Edita(Cliente cliente)
    {
        _db.Entry(cliente).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> Detalhes(int id)
    {
        return await _db.Cliente.Where(x => x.IdCliente == id).FirstOrDefaultAsync();
    }

    public async Task<List<Cliente>> ListaTodos()
    {
        return await _db.Cliente.Take(10).ToListAsync();

    }

    public int Delete(int id)
    {
        Cliente cliente = _db.Cliente.Where(x => x.IdCliente == id).FirstOrDefault();
        _db.Cliente.Remove(cliente);
        return _db.SaveChanges();
    }

}