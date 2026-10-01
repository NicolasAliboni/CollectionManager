using CollectionManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace CollectionManager.Web.Pages.Leituras;

public class CreateColecaoModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CreateColecaoModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public ColecaoLeituraFormViewModel Colecao { get; set; } = new();

    public List<FranquiaViewModel> Franquias { get; set; } = [];
    public List<EditoraViewModel> EditorasExterior { get; set; } = [];
    public List<EditoraViewModel> EditorasBrasil { get; set; } = [];

    public async Task OnGetAsync()
    {
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
            "api/ColecaoLeituras", Colecao);

        if (!response.IsSuccessStatusCode)
        {
            var mensagem = await response.Content.ReadAsStringAsync();

            ModelState.AddModelError(
                string.Empty,
                string.IsNullOrWhiteSpace(mensagem)
                    ? "Não foi possível cadastrar a coleção."
                    : mensagem);

            await CarregarListasAsync();

            return Page();
        }

        var colecaoCriada = await response.Content
            .ReadFromJsonAsync<ColecaoLeituraViewModel>();

        if (colecaoCriada == null || colecaoCriada.Id <= 0)
        {
            return RedirectToPage("Index");
        }

        return RedirectToPage("Colecao", new { id = colecaoCriada.Id });
    }

    private async Task CarregarListasAsync()
    {

        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        Franquias = await client.GetFromJsonAsync<List<FranquiaViewModel>>(
            "api/Franquias"
        ) ?? [];

        var todasEditoras = await client.GetFromJsonAsync<List<EditoraViewModel>>("api/Editoras") ?? [];

        EditorasExterior = todasEditoras.Where(e => e.Origem == OrigemEditora.Exterior).ToList();

        EditorasBrasil = todasEditoras.Where(e => e.Origem == OrigemEditora.Brasil).ToList();
    }
}
