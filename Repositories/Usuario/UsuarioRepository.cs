using Microsoft.Data.SqlClient;

public class UsuarioRepository : Database, IUsuarioRepository
{
    public void Create(Usuario usuario)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "INSERT INTO Usuarios VALUES (@nome, @email, @senha, @idade)";

        cmd.Parameters.AddWithValue("@nome", usuario.nome);
        cmd.Parameters.AddWithValue("@email", usuario.email);
        cmd.Parameters.AddWithValue("@senha", usuario.senha);
        cmd.Parameters.AddWithValue("@idade", usuario.idade);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "DELETE FROM Usuarios WHERE idUsuario = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    public List<Usuario> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT * FROM Usuarios";

        List<Usuario> usuarios =  new List<Usuario>();
        SqlDataReader reader = cmd.ExecuteReader();

        while(reader.Read())
        {
            Usuario usuario = new Usuario();

            usuario.idUsuario = reader.GetInt32(0);
            usuario.nome = reader.GetString(1);
            usuario.email = reader.GetString(2);
            usuario.senha = reader.GetString(3);
            usuario.idUsuario = reader.GetInt32(4);

            usuarios.Add(usuario);
        }
        return usuarios;
    }

    public Usuario Read(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT * FROM Usuarios WHERE idUsuario = @id";
        cmd.Parameters.AddWithValue("@id", id);

        Usuario usuario = new Usuario();
        SqlDataReader reader = cmd.ExecuteReader();

        if(reader.Read())
        {
            usuario.idUsuario = reader.GetInt32(0);
            usuario.nome = reader.GetString(1);
            usuario.email = reader.GetString(2);
            usuario.senha = reader.GetString(3);
            usuario.idUsuario = reader.GetInt32(4);

            return usuario;
        }
        return null;
    }

    public void Update(Usuario usuario, int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "UPDATE Usuarios SET nome = @nome, email = @email, senha = @senha, idade = @idade WHERE idUsuario = @id";

        cmd.Parameters.AddWithValue("@nome", usuario.nome);
        cmd.Parameters.AddWithValue("@email", usuario.email);
        cmd.Parameters.AddWithValue("@senha", usuario.senha);
        cmd.Parameters.AddWithValue("@idade", usuario.idade);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    public Usuario? Login(string email, string password)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT * FROM Usuarios WHERE email = @email AND senha = @password";
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@senha", password);

        Usuario usuario = new Usuario();
        SqlDataReader reader = cmd.ExecuteReader();

        if(reader.Read())
        {
            usuario.idUsuario = reader.GetInt32(0);
            usuario.nome = reader.GetString(1);
            usuario.email = reader.GetString(2);
            usuario.senha = reader.GetString(3);
            usuario.idUsuario = reader.GetInt32(4);

            return usuario;
        }
        return null;
    }

}