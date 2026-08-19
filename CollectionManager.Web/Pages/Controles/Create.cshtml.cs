using CollectionManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace CollectionManager.Web.Pages.Controles;

public class CreateModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CreateModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public ControleFormViewModel Controle { get; set; } = new();

    public List<EstadoViewModel> Estados { get; set; } = [];
    public List<FranquiaViewModel> Franquias { get; set; } = [];
    public List<MarcaViewModel> Marcas { get; set; } = [];
    public List<PlataformaViewModel> Plataformas { get; set; } = [];

    public async Task OnGetAsync()
    {
        await CarregarListasAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.PostAsJsonAsync(
            "api/Controles",
            Controle
        );

        if (!response.IsSuccessStatusCode)
        {
            await CarregarListasAsync();

            return Page();
        }

        return RedirectToPage("Index");
    }

    private async Task CarregarListasAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        Estados = await client.GetFromJsonAsync<List<EstadoViewModel>>(
            "api/Estados"
        ) ?? [];

        Franquias = await client.GetFromJsonAsync<List<FranquiaViewModel>>(
            "api/Franquias"
        ) ?? [];

        Marcas = await client.GetFromJsonAsync<List<MarcaViewModel>>(
            "api/Marcas"
        ) ?? [];

        Plataformas = await client.GetFromJsonAsync<List<PlataformaViewModel>>(
            "api/Plataformas"
        ) ?? [];
    }
}