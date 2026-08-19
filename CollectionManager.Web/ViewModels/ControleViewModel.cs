namespace CollectionManager.Web.ViewModels;

public class ControleViewModel
{
    public int ItemId { get; set; }
    public ItemViewModel Item { get; set; } = new();
    public string Modelo { get; set; } = string.Empty;
    public int MarcaId { get; set; }
    public MarcaViewModel Marca { get; set; } = new();
    public int PlataformaId { get; set; }
    public PlataformaViewModel Plataforma { get; set; } = new();

}

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

public class ControleFormViewModel
{
    public string Nome { get; set; } = string.Empty;
    public DateOnly DataLancamento { get; set; }
    public int EstadoId { get; set; }
    public string? CodigoEAN { get; set; }
    public DateOnly DataAquisicao { get; set; }
    public decimal? ValorAquisicao { get; set; }
    public int? FranquiaId { get; set; }
    public string? Observacoes { get; set; }
    public string Modelo { get; set; } = string.Empty;
    public int MarcaId { get; set; }
    public int PlataformaId { get; set; }
}