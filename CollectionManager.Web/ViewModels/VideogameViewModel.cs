namespace CollectionManager.Web.ViewModels;

public class VideogameViewModel
{
    public int ItemId { get; set; }
    public ItemViewModel Item { get; set; } = new();
    public int MarcaId { get; set; }
    public MarcaViewModel Marca { get; set; } = new();
}

public class VideogameFormViewModel
{
    public string Nome { get; set; } = string.Empty;
    public DateOnly DataLancamento { get; set; }
    public int EstadoId { get; set; }
    public string? CodigoEAN { get; set; }
    public DateOnly DataAquisicao { get; set; }
    public decimal? ValorAquisicao { get; set; }
    public int? FranquiaId { get; set; }
    public string? Observacoes { get; set; }
    public int MarcaId { get; set; }
}