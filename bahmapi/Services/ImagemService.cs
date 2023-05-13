#nullable disable

using bahmapi.Dtos;
using bahmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace bahmapi.Services;


public interface IImagemService
{

    public Task<Imagem> Novo(Imagem imagem);
    public Task<Imagem> Edita(Imagem imagem);
    public Task<Imagem> Detalhes(int id);
    public Task<List<Imagem>> ListaTodos();
    public int Delete(int id);

}

public partial class ImagemService : IImagemService
{
    private readonly DatabaseContext _db;

    public ImagemService()
    {
        _db = new DatabaseContext();
    }

    public async Task<Imagem> Novo(Imagem imagem)
    {
        _db.Imagem.Add(imagem);
        await _db.SaveChangesAsync();
        return imagem;
    }


    public async Task<Imagem> Edita(Imagem imagem)
    {
        _db.Entry(imagem).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return imagem;
    }

    public async Task<Imagem> Detalhes(int id)
    {
        return await _db.Imagem.Where(x => x.IdImagem == id).FirstOrDefaultAsync();
    }

    public async Task<List<Imagem>> ListaTodos()
    {
        return await _db.Imagem.ToListAsync();

    }

    public int Delete(int id)
    {
        Imagem imagem = _db.Imagem.Where(x => x.IdImagem == id).FirstOrDefault();
        _db.Imagem.Remove(imagem);
        return _db.SaveChanges();
    }

}