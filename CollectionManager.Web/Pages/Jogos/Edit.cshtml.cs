using CollectionManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace CollectionManager.Web.Pages.Jogos;

public class EditModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EditModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public int ItemId { get; set; }

    [BindProperty]
    public JogoFormViewModel Jogos { get; set; } = new();

    public List<EstadoViewModel> Estados { get; set; } = [];
    public List<FranquiaViewModel> Franquias { get; set; } = [];
    public List<MarcaViewModel> Marcas { get; set; } = [];
    public List<PlataformaViewModel> Plataformas { get; set; } = [];
    public List<StatusViewModel> Status { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var jogo = await client.GetFromJsonAsync<JogoViewModel>(
            $"api/Jogos/{id}"
        );

        if (jogo == null)
        {
            return NotFound();
        }

        ItemId = jogo.ItemId;

        Jogos = new JogoFormViewModel
        {
            Nome = jogo.Item.Nome,
            DataLancamento = jogo.Item.DataLancamento,
            EstadoId = jogo.Item.EstadoId,
            CodigoEAN = jogo.Item.CodigoEAN,
            DataAquisicao = jogo.Item.DataAquisicao,
            ValorAquisicao = jogo.Item.ValorAquisicao,
            FranquiaId = jogo.Item.FranquiaId,
            Observacoes = jogo.Item.Observacoes,
            MarcaId = jogo.MarcaId,
            PlataformaId = jogo.PlataformaId,
            StatusId = jogo.StatusId
        };

        await CarregarListasAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.PutAsJsonAsync(
            $"api/Jogos/{ItemId}",
            Jogos
        );

        if (!response.IsSuccessStatusCode)
        {
            await CarregarListasAsync();
            return Page();
        }

        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostDeleteAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.DeleteAsync(
            $"api/Jogos/{ItemId}"
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

        Status = await client.GetFromJsonAsync<List<StatusViewModel>>(
            "api/Status"
        ) ?? [];
    }
}
