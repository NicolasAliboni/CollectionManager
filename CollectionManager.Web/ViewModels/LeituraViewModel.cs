namespace CollectionManager.Web.ViewModels;

public enum TipoLeitura
{
    Manga = 1,
    Quadrinho = 2,
    Livro = 3
}

public class LeituraViewModel
{
        public int ItemId { get; set; }
        public ItemViewModel Item { get; set; } = new();
        public TipoLeitura Tipo { get; set; }
        public int EditoraExteriorId { get; set; }
        public EditoraViewModel EditoraExterior { get; set; } = new();
        public int EditoraBrasilId { get; set; }
        public EditoraViewModel EditoraBrasil { get; set; } = new();
        public string Autor { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public StatusViewModel Status { get; set; } = new();
        public string Lingua { get; set; } = string.Empty;
        public string? ISBN13 { get; set; }
        public int Volume { get; set; }
        public int VolumeAte { get; set; }
}

public class LeituraFormViewModel
{
    public string Nome { get; set; } = string.Empty;
    public DateOnly DataLancamento { get; set; }
    public int EstadoId { get; set; }
    public string? CodigoEAN { get; set; }
    public DateOnly DataAquisicao { get; set; }
    public decimal? ValorAquisicao { get; set; }
    public int? FranquiaId { get; set; }
    public string? Observacoes { get; set; }
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
