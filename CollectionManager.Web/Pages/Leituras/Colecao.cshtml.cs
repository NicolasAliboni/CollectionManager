using CollectionManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http.Json;

namespace CollectionManager.Web.Pages.Leituras;

public class ColecaoModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ColecaoModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public int Id { get; set; }

    [BindProperty]
    public ColecaoLeituraFormViewModel Colecao { get; set; } = new();

    public bool EmEdicao { get; set; }

    public List<LeituraViewModel> Leituras { get; set; } = [];
    public List<FranquiaViewModel> Franquias { get; set; } = [];
    public List<EditoraViewModel> EditorasExterior { get; set; } = [];
    public List<EditoraViewModel> EditorasBrasil { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Id = id;

        var encontrada = await CarregarColecaoAsync();

        if (!encontrada)
        {
            return NotFound("Coleção não encontrada.");
        }

        await CarregarListasAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostSalvarAsync(int id)
    {
        Id = id;
        EmEdicao = true;

        if (!ModelState.IsValid)
        {
            await CarregarListasAsync();
            return Page();
        }

        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.PutAsJsonAsync(
            $"api/ColecaoLeituras/{Id}", Colecao);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return NotFound("Coleção não encontrada.");
        }

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(
                string.Empty,
                "Não foi possível salvar a coleção. Verifique os dados.");

            await CarregarListasAsync();

            return Page();
        }

        return RedirectToPage("Colecao", new { id = Id });
    }

    public async Task<IActionResult> OnPostExcluirAsync(int id)
    {
        Id = id;

        // A exclusão não depende dos campos do formulário de edição.
        ModelState.Clear();

        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.DeleteAsync(
            $"api/ColecaoLeituras/{Id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("Index");
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return NotFound("Coleção não encontrada.");
        }

        var encontrada = await CarregarColecaoAsync();

        if (!encontrada)
        {
            return NotFound("Coleção não encontrada.");
        }

        await CarregarListasAsync();

        ModelState.AddModelError(
            string.Empty,
            "Não foi possível excluir a coleção.");

        return Page();
    }

    private async Task<bool> CarregarColecaoAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.GetAsync(
            $"api/ColecaoLeituras/{Id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return false;
        }

        response.EnsureSuccessStatusCode();

        var colecao = await response.Content
            .ReadFromJsonAsync<ColecaoLeituraViewModel>();

        if (colecao == null)
        {
            return false;
        }

        Colecao = new ColecaoLeituraFormViewModel
        {
            Nome = colecao.Nome,
            Tipo = colecao.Tipo,
            FranquiaId = colecao.FranquiaId,
            EditoraExteriorId = colecao.EditoraExteriorId,
            EditoraBrasilId = colecao.EditoraBrasilId,
            Autor = colecao.Autor,
            Lingua = colecao.Lingua,
            VolumeAte = colecao.VolumeAte,
            Observacoes = colecao.Observacoes
        };

        return true;
    }

    private async Task CarregarListasAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        Leituras = await client.GetFromJsonAsync<List<LeituraViewModel>>(
            $"api/Leituras?colecaoLeituraId={Id}"
        ) ?? [];

        Franquias = await client.GetFromJsonAsync<List<FranquiaViewModel>>(
            "api/Franquias"
        ) ?? [];

        var todasEditoras = await client
            .GetFromJsonAsync<List<EditoraViewModel>>(
                "api/Editoras"
            ) ?? [];

        EditorasExterior = todasEditoras
            .Where(e => e.Origem == OrigemEditora.Exterior)
            .ToList();

        EditorasBrasil = todasEditoras
            .Where(e => e.Origem == OrigemEditora.Brasil)
            .ToList();
    }
}