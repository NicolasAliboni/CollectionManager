using CollectionManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace CollectionManager.Web.Pages.Videogames;

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
    public VideogameFormViewModel Videogame { get; set; } = new();

    public List<EstadoViewModel> Estado { get; set; } = [];
    public List<FranquiaViewModel> Franquia { get; set; } = [];
    public List<MarcaViewModel> Marca { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var videogame = await client.GetFromJsonAsync<VideogameViewModel>(
            $"api/Videogames/{id}");

        if (videogame == null)
        {
            return NotFound();
        }

        ItemId = videogame.ItemId;

        Videogame = new VideogameFormViewModel
        {
            Nome = videogame.Item.Nome,
            DataLancamento = videogame.Item.DataLancamento,
            EstadoId = videogame.Item.EstadoId,
            CodigoEAN = videogame.Item.CodigoEAN,
            DataAquisicao = videogame.Item.DataAquisicao,
            ValorAquisicao = videogame.Item.ValorAquisicao,
            FranquiaId = videogame.Item.FranquiaId,
            Observacoes = videogame.Item.Observacoes,
            MarcaId = videogame.MarcaId
        };

        await CarregarListasAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.PutAsJsonAsync(
            $"api/Videogames/{ItemId}", Videogame);

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
            $"api/Videogames/{ItemId}");
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

        Estado = await client.GetFromJsonAsync<List<EstadoViewModel>>(
            "api/Estados") ?? [];

        Franquia = await client.GetFromJsonAsync<List<FranquiaViewModel>>(
            "api/Franquias") ?? [];

        Marca = await client.GetFromJsonAsync<List<MarcaViewModel>>(
            "api/Marcas") ?? [];
    }
}