namespace CollectionManager.Api.Models
{
    public enum OrigemEditora
    {
        Brasil = 1,
        Exterior = 2
    }
    public class Editora : EntidadeBase
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public OrigemEditora Origem { get; set; }
        public bool Ativo { get; set; }
    }
}
