namespace CollectionManager.Api.Models;

public abstract class EntidadeBase
{
    public DateTime DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public DateTime? DataExclusao { get; set; }
}