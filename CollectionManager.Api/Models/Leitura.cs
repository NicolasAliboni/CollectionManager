namespace CollectionManager.Api.Models
{
    public enum TipoLeitura
    {
        Manga = 1,
        Quadrinho = 2,
        Livro = 3
    }
    public class Leitura

    {
        public int ItemId { get; set; }
        public Item Item { get; set; } = null!;
        public TipoLeitura Tipo { get; set; }
        public int EditoraExteriorId { get; set; }
        public Editora EditoraExterior { get; set; } = null!;
        public int EditoraBrasilId { get; set; }
        public Editora EditoraBrasil { get; set; } = null!;
        public string Autor { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public Status Status { get; set; } = null!;
        public string Lingua { get; set; } = string.Empty;
        public string? ISBN13 { get; set; }
        public int Volume { get; set; }
        public int VolumeAte { get; set; }
    }
}