public class Receita
{
    public int idReceita {get;set;}
    public string nome {get;set;}
    public string imagem {get;set;}
    public string ingredientes {get;set;}
    public string modoPreparo {get;set;}
    public DateTime dataPostagem {get;set;} = DateTime.Now;
    public int categoriaId {get;set;}
    public int usuarioId {get; set;}
}