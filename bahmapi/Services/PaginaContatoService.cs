#nullable disable

using bahmapi.Dtos;
using bahmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace bahmapi.Services;


public interface IPaginaContatoService
{

    public Task<PaginaContato> Novo(PaginaContato paginaContato);
    public Task<PaginaContato> Edita(PaginaContato paginaContato);
    public Task<PaginaContato> Detalhes(int id);
    public Task<List<PaginaContato>> ListaTodos();
    public int Delete(int id);
    public Task<List<PaginaContato>> ListaTodosContatos(int id);
    public void AlteraOrdemUp(int paginaId, int IdPaginaContato);
    public void AlteraOrdemUpDown(int paginaId, int IdPaginaContato);

}

public partial class PaginaContatoService : IPaginaContatoService
{
    private readonly DatabaseContext _db;

    public PaginaContatoService()
    {
        _db = new DatabaseContext();
    }

    public async Task<PaginaContato> Novo(PaginaContato paginaContato)
    {
        _db.PaginaContato.Add(paginaContato);
        await _db.SaveChangesAsync();
        return paginaContato;
    }


    public async Task<PaginaContato> Edita(PaginaContato paginaContato)
    {
        _db.Entry(paginaContato).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return paginaContato;
    }

    public async Task<PaginaContato> Detalhes(int id)
    {
        return await _db.PaginaContato.Where(x => x.IdPaginaContato == id).FirstOrDefaultAsync();
    }

    public async Task<List<PaginaContato>> ListaTodos()
    {
        return await _db.PaginaContato.Take(10).ToListAsync();

    }

    public int Delete(int id)
    {
        PaginaContato paginaContato = _db.PaginaContato.Where(x => x.IdPaginaContato == id).FirstOrDefault();
        _db.PaginaContato.Remove(paginaContato);
        return _db.SaveChanges();
    }

    public async Task<List<PaginaContato>> ListaTodosContatos(int id)
    {
        return await _db.PaginaContato.Where(paginaContato => paginaContato.PaginaId == id)
                                                   .Join(_db.Contato, contato => contato.ContatoId,
                                                   contato => contato.IdContato,
                                                   (paginaContato, contato) => new PaginaContato
                                                   {
                                                       IdPaginaContato = paginaContato.IdPaginaContato,
                                                       Contato = contato
                                                   }).ToListAsync();
    }

    public async void AlteraOrdemUp(int paginaId, int IdPaginaContato)
    {

        List<PaginaContato> paginaContatos = _db.PaginaContato.Where(x => x.PaginaId == paginaId).ToList();
        int paginaContatoIndex = paginaContatos.FindIndex(x => x.IdPaginaContato == IdPaginaContato);
        int aux = paginaContatos[paginaContatoIndex - 1].ContatoId;
        paginaContatos[paginaContatoIndex - 1].ContatoId = paginaContatos[paginaContatoIndex].ContatoId;
        paginaContatos[paginaContatoIndex].ContatoId = aux;
        await _db.SaveChangesAsync();
    }

    public async void AlteraOrdemUpDown(int paginaId, int IdPaginaContato)
    {
        List<PaginaContato> paginaContatos = _db.PaginaContato.Where(x => x.PaginaId == paginaId).ToList();
        var paginaContatoIndex = paginaContatos.FindIndex(x => x.IdPaginaContato == IdPaginaContato);
        int aux = paginaContatos[paginaContatoIndex + 1].ContatoId;
        paginaContatos[paginaContatoIndex + 1].ContatoId = paginaContatos[paginaContatoIndex].ContatoId;
        paginaContatos[paginaContatoIndex].ContatoId = aux;
        await _db.SaveChangesAsync();
    }
}