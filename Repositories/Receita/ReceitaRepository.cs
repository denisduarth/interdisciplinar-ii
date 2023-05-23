using Microsoft.Data.SqlClient;
public class ReceitaRepository : Database, IReceitaRepository
{
    public List<Receita> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT * FROM Receitas";

        SqlDataReader reader = cmd.ExecuteReader();
        List<Receita> receitas = new List<Receita>();

        while(reader.Read())
        {
            Receita receita = new Receita();
            
            receita.idReceita = reader.GetInt32(0);
            receita.nome =      reader.GetString(1);
            receita.imagem =    reader.GetString(2);
            receita.descricao = reader.GetString(3);

            receitas.Add(receita);
        }
        return receitas;
    }

    public Receita Read(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT * FROM Receitas WHERE idReceita = @id";
        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        if(reader.Read())
        {
            Receita receita = new Receita();
            
            receita.idReceita = reader.GetInt32(0);
            receita.nome =      reader.GetString(1);
            receita.imagem =    reader.GetString(2);
            receita.descricao = reader.GetString(3);
            
            return receita;
        }
        return null;
    }

    public void Create(Receita receita, Categoria categoria)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "INSERT INTO Receitas VALUES (@nome, @imagem, @descricao, @categoriaId)";
        
        cmd.Parameters.AddWithValue("@nome",        receita.nome);
        cmd.Parameters.AddWithValue("@imagem",      receita.imagem);
        cmd.Parameters.AddWithValue("@descricao",   receita.descricao);
        cmd.Parameters.AddWithValue("@categoriaId", categoria.idCategoria);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "DELETE FROM Receitas WHERE idReceita = @id";

        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public void Update(Receita receita, int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "UPDATE Receitas SET nome = @nome, imagem = @imagem, descricao = @descricao WHERE idReceita = @id";

        cmd.Parameters.AddWithValue("@nome", receita.nome);
        cmd.Parameters.AddWithValue("@imagem", receita.imagem);
        cmd.Parameters.AddWithValue("@descricao", receita.descricao);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }
}