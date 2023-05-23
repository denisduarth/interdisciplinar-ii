public interface IUsuarioRepository
{
    public List<Usuario> Read();
    public Usuario Read(int id);
    public void Create(Usuario usuario);
    public void Delete(int id);
    public void Update(Usuario usuario, int id);
    public Usuario? Login(string email, string password);
}