public interface IReceitaRepository
{
    List<Receita> Read();
    Receita Read(int id);
    List<Receita> GetByUsuarioId(int usuarioId);
    void Create(Receita receita, int usuarioId);
    void Delete(int id);
    void Update(Receita receita, int id);
    List<Receita> Search(string busca);
    List<Receita> GetByCategoria(int usuarioId);
}