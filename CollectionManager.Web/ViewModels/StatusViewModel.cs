namespace CollectionManager.Web.ViewModels
{
    public enum TipoItem
    {
        Jogo = 1,
        Leitura = 2,
        Controle = 3,
        Videogame = 4
    }

    public class StatusViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public TipoItem Tipo { get; set; }
    }
}