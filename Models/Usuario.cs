public class Usuario
{
    public int idUsuario {get; set;}
    public string nome {get; set;}
    public string email {get; set;}
    public string senha {get; set;}
    public int idade {get; set;}
    public string? imagem {get; set;}

    public List<Receita> receitasCriadas {get; set;}

    public Usuario()
    {
        receitasCriadas = new List<Receita>();
    }
}