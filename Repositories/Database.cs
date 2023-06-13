using Microsoft.Data.SqlClient;
using System;

public abstract class Database : IDisposable
{
    protected SqlConnection conn;

    public Database()
    {   
        string connectionString = $"Data Source=DESKTOP-A7IQO85\\SQLDENIS; Initial Catalog=VeganoBD; Integrated Security=true; TrustServerCertificate=true; MultipleActiveResultSets=True";

        conn = new SqlConnection(connectionString);
        conn.Open();
    }

    public void Dispose()
    {
        conn.Close();
        conn.Dispose();
    }
}