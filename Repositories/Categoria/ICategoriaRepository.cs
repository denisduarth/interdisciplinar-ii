public interface ICategoriaRepository
{
    List<Categoria> Read();
    Categoria Read(int id);
    void Create(Categoria categoria);
    void Delete(int id);
    void Update(Categoria categoria, int id);
}