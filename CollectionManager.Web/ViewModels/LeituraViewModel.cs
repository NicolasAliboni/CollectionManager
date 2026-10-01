namespace CollectionManager.Web.ViewModels;

public enum StatusLeitura
{
    ALer = 1,
    Lendo = 2,
    Lido = 3
}

public class LeituraViewModel
{
    public int Id { get; set; }
    public string? Titulo { get; set; }
    public DateOnly DataLancamento { get; set; }
    public DateOnly DataAquisicao { get; set; }
    public string? CodigoEAN { get; set; }
    public string? ISBN13 { get; set; }
    public decimal Volume { get; set; }
    public decimal? ValorAquisicao { get; set; }
    public string? Observacoes { get; set; }
    public int ColecaoLeituraId { get; set; }
    public StatusLeitura Status { get; set; }
    public int EstadoId { get; set; }
    public EstadoViewModel Estado { get; set; } = new();
}

public class LeituraFormViewModel
{
    public int ColecaoLeituraId { get; set; }
    public string? Titulo { get; set; }
    public DateOnly DataLancamento { get; set; }
    public DateOnly DataAquisicao { get; set; }
    public StatusLeitura Status { get; set; } = StatusLeitura.ALer;
    public int EstadoId { get; set; }
    public string? CodigoEAN { get; set; }
    public string? ISBN13 { get; set; }
    public decimal Volume { get; set; }
    public decimal? ValorAquisicao { get; set; }
    public string? Observacoes { get; set; }
}
