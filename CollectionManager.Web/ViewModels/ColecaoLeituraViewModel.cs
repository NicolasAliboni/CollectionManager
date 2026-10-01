namespace CollectionManager.Web.ViewModels;

public enum TipoLeitura
{
    Manga = 1,
    Quadrinho = 2,
    Livro = 3
}

public class ColecaoLeituraViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string Lingua { get; set; } = string.Empty;
    public int VolumeAte { get; set; }
    public string? Observacoes { get; set; }
    public TipoLeitura Tipo { get; set; }
    public int? FranquiaId { get; set; }
    public FranquiaViewModel? Franquia { get; set; }
    public int EditoraExteriorId { get; set; }
    public EditoraViewModel EditoraExterior { get; set; } = new();
    public int EditoraBrasilId { get; set; }
    public EditoraViewModel EditoraBrasil { get; set; } = new();
}

public class ColecaoLeituraFormViewModel
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
