using CollectionManager.Api.Models;

namespace CollectionManager.Api.DTOs;

public class LeituraCreateDto
{
    // Dados de Leitura
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
