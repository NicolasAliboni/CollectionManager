namespace CollectionManager.Api.DTOs;

public class ControleCreateDto
{
    // Dados de Item

    public string Nome { get; set; } = string.Empty;

    public DateOnly DataLancamento { get; set; }

    public int EstadoId { get; set; }

    public string? CodigoEAN { get; set; }

    public DateOnly DataAquisicao { get; set; }

    public decimal? ValorAquisicao { get; set; }

    public int? FranquiaId { get; set; }

    public string? Observacoes { get; set; }

    // Dados específicos de Controle

    public string Modelo { get; set; } = string.Empty;

    public int MarcaId { get; set; }

    public int PlataformaId { get; set; }
}