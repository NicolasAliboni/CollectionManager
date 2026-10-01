using CollectionManager.Api.Models;

namespace CollectionManager.Api.DTOs;

public class ColecaoLeituraCreateDto
{
    public string Nome { get; set; } = string.Empty;
    public TipoLeitura Tipo { get; set; }
    public int? FranquiaId { get; set; }
    public int EditoraExteriorId { get; set; }
    public int EditoraBrasilId { get; set; }
    public string Autor { get; set; } = string.Empty;
    public string Lingua { get; set; } = string.Empty;
    public int VolumeAte { get; set; }
    public string? Observacoes { get; set; }
}