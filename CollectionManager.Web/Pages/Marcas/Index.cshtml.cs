using CollectionManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace CollectionManager.Web.Pages.Marcas;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<MarcaViewModel> Marcas { get; set; } = [];

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var marcas = await client.GetFromJsonAsync<List<MarcaViewModel>>(
            "api/Marcas"
        );

        Marcas = marcas ?? [];
    }
}