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

    public async Task OnGetAsync(int colecaoLeituraId)
    {
        Leitura.ColecaoLeituraId = colecaoLeituraId;

        await CarregarListasAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await CarregarListasAsync();
            return Page();
        }

        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.PostAsJsonAsync(
            "api/Leituras",Leitura);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(
                string.Empty,
                "Não foi possível cadastrar o volume. Verifique os dados.");

            await CarregarListasAsync();
            return Page();
        }

        return RedirectToPage(
            "Colecao",
            new { id = Leitura.ColecaoLeituraId });
    }

    private async Task CarregarListasAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var todosEstados = await client
            .GetFromJsonAsync<List<EstadoViewModel>>(
                "api/Estados"
            ) ?? [];

        Estados = todosEstados
            .Where(e => e.TipoColecao == TipoColecao.Leitura)
            .ToList();
    }
}
