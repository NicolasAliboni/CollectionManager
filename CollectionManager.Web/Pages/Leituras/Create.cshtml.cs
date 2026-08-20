using CollectionManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace CollectionManager.Web.Pages.Leituras;

public class CreateModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CreateModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public LeituraFormViewModel Leitura { get; set; } = new();

    public List<EstadoViewModel> Estados { get; set; } = [];
    public List<FranquiaViewModel> Franquias { get; set; } = [];
    public List<EditoraViewModel> EditoraExterior { get; set; } = [];
    public List<EditoraViewModel> EditoraBrasil { get; set; } = [];
    public List<StatusViewModel> Status { get; set; } = [];

    public async Task OnGetAsync()
    {
        await CarregarListasAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.PostAsJsonAsync(
            "api/Leituras",Leitura);

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

        var todasEditoras = await client.GetFromJsonAsync<List<EditoraViewModel>>("api/Editoras") ?? [];

        EditoraExterior = todasEditoras.Where(e => e.Origem == OrigemEditora.Exterior).ToList();

        EditoraBrasil = todasEditoras.Where(e => e.Origem == OrigemEditora.Brasil).ToList();

        var todosStatus = await client.GetFromJsonAsync<List<StatusViewModel>>(
            "api/Status"
        ) ?? [];

        Status = todosStatus
            .Where(s => s.Tipo == TipoItem.Leitura)
            .ToList();
    }
}
