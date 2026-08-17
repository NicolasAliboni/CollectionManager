namespace CollectionManager.Api.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateOnly DataLancamento { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; } = null!;
        public string? CodigoEAN { get; set; }
        public DateOnly DataAquisicao { get; set; }
        public decimal? ValorAquisicao { get; set; }
        public int? FranquiaId { get; set; }
        public Franquia? Franquia { get; set; }
        public string? Observacoes { get; set; }
    }
}
