namespace CollectionManager.Api.Models
{
    public enum TipoLeitura
    {
        Manga = 1,
        Quadrinho = 2,
        Livro = 3
    }

    public class ColecaoLeitura : EntidadeBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public TipoLeitura Tipo { get; set; }
        public int? FranquiaId { get; set; }
        public Franquia? Franquia { get; set; }
        public int EditoraExteriorId { get; set; }
        public Editora EditoraExterior { get; set; } = null!;
        public int EditoraBrasilId { get; set; }
        public Editora EditoraBrasil { get; set; } = null!;
        public string Autor { get; set; } = string.Empty;
        public string Lingua { get; set; } = string.Empty;
        public int VolumeAte { get; set; }
        public string? Observacoes { get; set; }
    }
}