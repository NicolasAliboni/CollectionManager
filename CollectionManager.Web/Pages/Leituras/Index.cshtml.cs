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

    public List<ColecaoLeituraViewModel> Colecoes { get; set; } = [];

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var colecaoLeituras = await client
            .GetFromJsonAsync<List<ColecaoLeituraViewModel>>("api/ColecaoLeituras") ?? [];

        Colecoes = colecaoLeituras
            .OrderBy(c => c.Nome)
            .ToList();
    }
}