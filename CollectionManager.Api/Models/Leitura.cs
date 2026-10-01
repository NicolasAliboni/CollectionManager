namespace CollectionManager.Api.Models
{
    public enum StatusLeitura
    {
        ALer = 1,
        Lendo = 2,
        Lido = 3
    }

    public class Leitura : EntidadeBase
    {
        public int Id { get; set; }
        public int ColecaoLeituraId { get; set; }
        public ColecaoLeitura ColecaoLeitura { get; set; } = null!;
        public string? Titulo { get; set; }
        public DateOnly DataLancamento { get; set; }
        public DateOnly DataAquisicao { get; set; }
        public StatusLeitura Status { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; } = null!;
        public string? CodigoEAN { get; set; }
        public string? ISBN13 { get; set; }
        public decimal Volume { get; set; }
        public decimal? ValorAquisicao { get; set; }
        public string? Observacoes { get; set; }
    }
}