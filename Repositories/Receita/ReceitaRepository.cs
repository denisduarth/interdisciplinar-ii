using System.Data;
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
            
            receita.idReceita =     reader.GetInt32(0);
            receita.nome =          reader.GetString(1);
            receita.imagem =        reader.GetString(2);
            receita.ingredientes =  reader.GetString(3);
            receita.modoPreparo =   reader.GetString(4);

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
            
            receita.idReceita =     reader.GetInt32(0);
            receita.nome =          reader.GetString(1);
            receita.imagem =        reader.GetString(2);
            receita.ingredientes =  reader.GetString(3);
            receita.modoPreparo =   reader.GetString(4);
            
            return receita;
        }
        return null;
    }

    public List<Receita> GetByUsuarioId(int usuarioId)
    {
        List<Receita> receitas = new List<Receita>();

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Receitas WHERE usuarioId = @usuarioId";
            cmd.Parameters.AddWithValue("@usuarioId", usuarioId);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Receita receita = new Receita();

                    receita.idReceita = reader.GetInt32(0);
                    receita.nome = reader.GetString(1);
                    receita.imagem = reader.GetString(2);
                    receita.ingredientes = reader.GetString(3);
                    receita.modoPreparo = reader.GetString(4);

                    receitas.Add(receita);
                }
                
                reader.Close();
            }
            
            return receitas;
        }

    }

    public List<Receita> GetByCategoria(int categoriaId)
    {
        List<Receita> receitas = new List<Receita>();

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM Receitas WHERE categoriaId = @categoriaId";
            cmd.Parameters.AddWithValue("@categoriaId", categoriaId);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Receita receita = new Receita();

                    receita.idReceita = reader.GetInt32(0);
                    receita.nome = reader.GetString(1);
                    receita.imagem = reader.GetString(2);
                    receita.ingredientes = reader.GetString(3);
                    receita.modoPreparo = reader.GetString(4);

                    receitas.Add(receita);
                }
                
                reader.Close();
            }
            
        }
        return receitas;
    }

    public void Create(Receita receita, int usuarioId)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = @"INSERT INTO Receitas(nome, imagem, ingredientes, modoPreparo, categoriaId, usuarioId, dataPostagem) 
        VALUES (@nome, @imagem, @ingredientes, @modoPreparo, @categoriaId, @usuarioId, @dataPostagem)";
        
        cmd.Parameters.AddWithValue("@nome",            receita.nome);
        cmd.Parameters.AddWithValue("@imagem",          receita.imagem);
        cmd.Parameters.AddWithValue("@ingredientes",    receita.ingredientes);
        cmd.Parameters.AddWithValue("@modoPreparo",     receita.modoPreparo);
        cmd.Parameters.AddWithValue("@categoriaId",     receita.categoriaId);
        cmd.Parameters.AddWithValue("@usuarioId",       usuarioId);
        cmd.Parameters.AddWithValue("@dataPostagem",    receita.dataPostagem);
        
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
        cmd.CommandText = @"UPDATE Receitas 
        SET nome = @nome, 
        imagem = @imagem, 
        ingredientes = @ingredientes,
        modoPreparo = @modoPreparo
        WHERE idReceita = @id";

        cmd.Parameters.AddWithValue("@nome",            receita.nome);
        cmd.Parameters.AddWithValue("@imagem",          receita.imagem);
        cmd.Parameters.AddWithValue("@ingredientes",    receita.ingredientes);
        cmd.Parameters.AddWithValue("@modoPreparo",     receita.modoPreparo);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    public List<Receita> Search(string busca)
    {
        List<Receita> receitas = new List<Receita>();

        using(SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "SELECT * FROM Receitas WHERE nome LIKE '%' + @busca + '%'";
            cmd.Connection = conn;
            cmd.Parameters.Add("@busca", SqlDbType.NVarChar).Value = busca;
            
            using(SqlDataReader reader = cmd.ExecuteReader())
            {
                while(reader.Read())
                {
                    Receita receita = new Receita();

                    receita.idReceita = reader.GetInt32(0);
                    receita.nome = reader.GetString(1);
                    receita.ingredientes = reader.GetString(2);
                    receita.modoPreparo = reader.GetString(3);
                    receita.imagem = reader.GetString(4);

                    receitas.Add(receita);
                }
                reader.Close();
            }
        }

        return receitas;
    }
}