using bahmapi.Entities;
using Microsoft.EntityFrameworkCore;

namespace bahmapi.Services;
#nullable disable

public interface IUsuarioService
{
    public Task<List<Usuario>> ListaTodos();
    public Task<Usuario> Detalhes(int id);
    public int Delete(int id);
    public Usuario Novo(Usuario usuario);

    public Usuario Edita(Usuario usuario);

    public Usuario Login(string Email, string Senha);
}

public partial class UsuarioService : IUsuarioService
{
    private readonly DatabaseContext _db;

    public UsuarioService(DatabaseContext db)
    {
        _db = db;
    }

    public async Task<Usuario> Detalhes(int id)
    {
        return await _db.Usuario.Where(x => x.IdUsuario == id).FirstOrDefaultAsync();
    }

    public Usuario Login(string Email, string Senha)
    {
        Usuario usuario = _db.Usuario.Where(x => x.EmailUsuario == Email && x.SenhaUsuario == Senha).FirstOrDefault();
        if (usuario == null)
            throw new Exception("Email ou senha incorretos!");
        return usuario;

    }

    public int Delete(int id)
    {
        Usuario usuario = _db.Usuario.Where(x => x.IdUsuario == id).FirstOrDefault();
        _db.Usuario.Remove(usuario);
        return _db.SaveChanges();
    }

    public async Task<List<Usuario>> ListaTodos()
    {
        return await _db.Usuario.ToListAsync();

    }
    public Usuario Novo(Usuario usuario)
    {
        Usuario _usuario = usuario;
        _db.Add(_usuario);
        _db.SaveChanges();
        return _usuario;
    }

    public Usuario Edita(Usuario usuario)
    {
        _db.Entry(usuario).State = EntityState.Modified;
        _db.SaveChanges();
        return usuario;
    }
}