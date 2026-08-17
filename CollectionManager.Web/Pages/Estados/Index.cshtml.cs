using CollectionManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace CollectionManager.Web.Pages.Estados;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<EstadoViewModel> Estados { get; set; } = [];

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var estados = await client.GetFromJsonAsync<List<EstadoViewModel>>(
            "api/Estados"
        );

        Estados = estados ?? [];
    }
}