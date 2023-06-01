using Microsoft.Data.SqlClient;

public abstract class Database : IDisposable
{
    protected SqlConnection conn;

    /*
        BANCOS:
        - DESKTOP-A7IQO85\\SQLDENIS - Meu banco
        - DESKTOP-U2BFRBE - Banco lab 3

    */
    public Database()
    {
        string connectionString = "Data Source=LAB0208; Initial Catalog=VeganoBD; Integrated Security=true; TrustServerCertificate=true";

        conn = new SqlConnection(connectionString);
        conn.Open();
    }

    public void Dispose()
    {
        conn.Close();
    }
}