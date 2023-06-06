using Microsoft.Data.SqlClient;

public class CategoriaRepository : Database, ICategoriaRepository
{
    public List<Categoria> Read()
    {
        SqlCommand cmd = new SqlCommand("SELECT * FROM Categorias", conn);
        List<Categoria> categorias = new List<Categoria>();

        using(SqlDataReader reader = cmd.ExecuteReader())
        {
            while(reader.Read())
            {
                Categoria categoria = new Categoria();

                categoria.idCategoria = reader.GetInt32(0);
                categoria.nome =        reader.GetString(1);

                categorias.Add(categoria);
            }
            return categorias;
        }
    }

    public Categoria Read(int id)
    {
        SqlCommand cmd = new SqlCommand("SELECT * FROM Categorias WHERE idCategoria = @id", conn);
        cmd.Parameters.AddWithValue("@id",id);

        using(SqlDataReader reader = cmd.ExecuteReader())
        {
            if(reader.Read())
            {
                Categoria categoria = new Categoria();

                categoria.idCategoria = reader.GetInt32(0);
                categoria.nome =        reader.GetString(1);

                return categoria;
            }
            return null;
        }
    }

    public void Create(Categoria categoria)
    {
       SqlCommand cmd = new SqlCommand();
       cmd.CommandText = "INSERT INTO Categorias VALUES (@nome)";
       cmd.Connection = conn;

       cmd.Parameters.AddWithValue("@nome", categoria.nome);
       cmd.ExecuteNonQuery();
    }
    
    public void Delete(int id)
    {
       SqlCommand cmd = new SqlCommand("DELETE FROM Categorias WHERE idCategoria = @id", conn);
       cmd.Parameters.AddWithValue("@id", id);
       cmd.ExecuteNonQuery();
    }
    
    public void Update(Categoria categoria, int id)
    {
       SqlCommand cmd = new SqlCommand("UPDATE Categorias SET nome = @nome WHERE idCategoria = @id", conn);
       cmd.Parameters.AddWithValue("@nome", categoria.nome);
       cmd.Parameters.AddWithValue("@id", id);
       cmd.ExecuteNonQuery();
    }
}