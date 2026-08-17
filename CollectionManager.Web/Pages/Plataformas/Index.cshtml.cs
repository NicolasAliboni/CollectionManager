using CollectionManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace CollectionManager.Web.Pages.Plataformas;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<PlataformaViewModel> Plataformas { get; set; } = [];

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var plataformas = await client.GetFromJsonAsync<List<PlataformaViewModel>>(
            "api/Plataformas"
        );

        Plataformas = plataformas ?? [];
    }
}