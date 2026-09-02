namespace CollectionManager.Web.ViewModels
{
    public enum TipoColecao
    {
        Jogo = 1,
        Leitura = 2,
        Controle = 3,
        Videogame = 4
    }
    public class EstadoViewModel
    {
        public int Id { get; set; }
    
        public string Nome { get; set; } = string.Empty;
    
        public TipoColecao TipoColecao { get; set; }
    }
}