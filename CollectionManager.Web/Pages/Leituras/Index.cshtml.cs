using CollectionManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace CollectionManager.Web.Pages.Leituras;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<LeituraFranquiaViewModel> Franquias { get; set; } = [];

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var leituras = await client
            .GetFromJsonAsync<List<LeituraViewModel>>("api/Leituras") ?? [];

        Franquias = leituras
            .GroupBy(l => new
            {
                l.Item.FranquiaId,
                Nome = l.Item.Franquia?.Nome ?? "Sem Franquia"
            })
            .Select(g => new LeituraFranquiaViewModel
            {
                FranquiaId = g.Key.FranquiaId,

                Nome = g.Key.Nome,

                QuantidadeVolumes = g
                    .Select(l => l.Volume)
                    .Distinct()
                    .Count(),

                VolumeAte = g.Key.FranquiaId.HasValue
                    ? g.Max(l => l.VolumeAte)
                    : 0
            })
            .OrderBy(f => f.Nome == "Sem Franquia")
            .ThenBy(f => f.Nome)
            .ToList();
    }
}