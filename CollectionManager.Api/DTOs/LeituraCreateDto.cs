using CollectionManager.Api.Models;

namespace CollectionManager.Api.DTOs;

public class LeituraCreateDto
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
    // Dados específicos de Leitura
    public TipoLeitura Tipo { get; set; }
    public int EditoraExteriorId { get; set; }
    public int EditoraBrasilId { get; set; }
    public string Autor { get; set; } = string.Empty;
    public int StatusId { get; set; }
    public string Lingua { get; set; } = string.Empty;
    public string? ISBN13 { get; set; }
    public int Volume { get; set; }
    public int VolumeAte { get; set; }
}
