namespace CollectionManager.Api.Models;

public class Controle
{
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;
    public string Modelo { get; set; } = string.Empty;
    public int MarcaId { get; set; }
    public Marca Marca { get; set; } = null!;
    public int PlataformaId { get; set; }
    public Plataforma Plataforma { get; set; } = null!;
}