namespace CollectionManager.Api.DTOs;

public class VideogameCreateDto
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

    // Dados específicos de Videogame

    public int MarcaId { get; set; }
}