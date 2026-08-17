namespace CollectionManager.Api.Models
{
    public class Videogame
    {
        public int ItemId { get; set; }
        public Item Item { get; set; } = null!;
        public int MarcaId { get; set; }
        public Marca Marca { get; set; } = null!;
    }
}
