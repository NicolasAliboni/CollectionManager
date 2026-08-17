namespace CollectionManager.Api.Models
{
    public class Jogo
    {
        public int ItemId { get; set; }
        public Item Item { get; set; } = null!;
        public int MarcaId { get; set; }
        public Marca Marca { get; set; } = null!;
        public int PlataformaId { get; set; }
        public Plataforma Plataforma { get; set; } = null!;
        public int StatusId { get; set; }
        public Status Status { get; set; } = null!;
    }
}
