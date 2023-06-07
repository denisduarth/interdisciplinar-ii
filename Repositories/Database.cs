using Microsoft.Data.SqlClient;

public abstract class Database : IDisposable
{
    protected SqlConnection conn;

    public Database()
    {
        string connectionString = "Data Source=LAB0402\\MSSQLSERVER02; Initial Catalog=VeganoBD; Integrated Security=true; TrustServerCertificate=true";

        conn = new SqlConnection(connectionString);
        conn.Open();
    }

    public void Dispose()
    {
        conn.Close();
        conn.Dispose();
    }
}