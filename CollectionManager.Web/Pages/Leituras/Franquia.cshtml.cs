using CollectionManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace CollectionManager.Web.Pages.Leituras;

public class FranquiaModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FranquiaModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public string NomeFranquia { get; set; } = string.Empty;

    public int? FranquiaId { get; set; }

    public int QuantidadeVolumes { get; set; }

    public int VolumeAte { get; set; }

    public List<LeituraViewModel> Leituras { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var leituras = await client
            .GetFromJsonAsync<List<LeituraViewModel>>("api/Leituras") ?? [];

        FranquiaId = id;

        if (id.HasValue)
        {
            Leituras = leituras
                .Where(l => l.Item.FranquiaId == id.Value)
                .OrderBy(l => l.Volume)
                .ToList();

            if (Leituras.Count == 0)
            {
                return NotFound();
            }

            NomeFranquia =
                Leituras.First().Item.Franquia?.Nome ?? "Franquia";

            QuantidadeVolumes = Leituras
                .Select(l => l.Volume)
                .Distinct()
                .Count();

            VolumeAte = Leituras.Max(l => l.VolumeAte);
        }
        else
        {
            Leituras = leituras
                .Where(l => !l.Item.FranquiaId.HasValue)
                .OrderBy(l => l.Item.Nome)
                .ToList();

            NomeFranquia = "Sem Franquia";

            QuantidadeVolumes = Leituras.Count;

            VolumeAte = 0;
        }

        return Page();
    }
}