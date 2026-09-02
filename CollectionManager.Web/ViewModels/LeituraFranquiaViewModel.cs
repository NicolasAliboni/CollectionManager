namespace CollectionManager.Web.ViewModels;

public class LeituraFranquiaViewModel
{
    public int? FranquiaId { get; set; }

    public string Nome { get; set; } = string.Empty;

    public int QuantidadeVolumes { get; set; }

    public int VolumeAte { get; set; }
}