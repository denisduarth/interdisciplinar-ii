public class Receita
{
    public int idReceita {get;set;}
    public string nome {get;set;}
    public string imagem {get;set;}
    public string descricao {get;set;}

    /*
        Estabelecido apenas com o getter sem o setter, o atributo
        dataPostagem está programado apenas para ser um "readonly"
        que possui um valor padrão da data atual no momento em que 
        a publicação da receita foi feita.
    */
    public DateTime dataPostagem {get;} = DateTime.Now;
}