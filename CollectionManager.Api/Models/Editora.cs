namespace CollectionManager.Api.Models
{
    public enum OrigemEditora
    {
        Brasil = 1,
        Exterior = 2
    }
    public class Editora
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public OrigemEditora Origem { get; set; }
    }
}
