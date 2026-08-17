namespace CollectionManager.Api.Models;

public enum TipoItem
{
    Jogo = 1,
    Leitura = 2,
    Controle = 3,
    Videogame = 4
}

public class Status
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public TipoItem Tipo { get; set; }
}