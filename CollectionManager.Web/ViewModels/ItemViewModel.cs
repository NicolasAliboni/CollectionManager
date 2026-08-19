namespace CollectionManager.Web.ViewModels;

public class ItemViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateOnly DataLancamento { get; set; }
    public int EstadoId { get; set; }
    public EstadoViewModel Estado { get; set; } = new();
    public string? CodigoEAN { get; set; }
    public DateOnly DataAquisicao { get; set; }
    public decimal? ValorAquisicao { get; set; }
    public int? FranquiaId { get; set; }
    public FranquiaViewModel? Franquia { get; set; }
    public string? Observacoes { get; set; }
}