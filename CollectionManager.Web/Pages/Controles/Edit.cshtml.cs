using CollectionManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace CollectionManager.Web.Pages.Controles;

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
    public ControleFormViewModel Controle { get; set; } = new();

    public List<EstadoViewModel> Estados { get; set; } = [];
    public List<FranquiaViewModel> Franquias { get; set; } = [];
    public List<MarcaViewModel> Marcas { get; set; } = [];
    public List<PlataformaViewModel> Plataformas { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var controle = await client.GetFromJsonAsync<ControleViewModel>(
            $"api/Controles/{id}"
        );

        if (controle == null)
        {
            return NotFound();
        }

        ItemId = controle.ItemId;

        Controle = new ControleFormViewModel
        {
            Nome = controle.Item.Nome,
            DataLancamento = controle.Item.DataLancamento,
            EstadoId = controle.Item.EstadoId,
            CodigoEAN = controle.Item.CodigoEAN,
            DataAquisicao = controle.Item.DataAquisicao,
            ValorAquisicao = controle.Item.ValorAquisicao,
            FranquiaId = controle.Item.FranquiaId,
            Observacoes = controle.Item.Observacoes,
            Modelo = controle.Modelo,
            MarcaId = controle.MarcaId,
            PlataformaId = controle.PlataformaId
        };

        await CarregarListasAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.PutAsJsonAsync(
            $"api/Controles/{ItemId}",
            Controle
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
            $"api/Controles/{ItemId}"
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