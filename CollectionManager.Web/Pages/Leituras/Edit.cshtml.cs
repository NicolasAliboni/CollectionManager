using CollectionManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace CollectionManager.Web.Pages.Leituras;

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
    public LeituraFormViewModel Leitura { get; set; } = new();

    public List<EstadoViewModel> Estados { get; set; } = [];
    public List<FranquiaViewModel> Franquias { get; set; } = [];
    public List<EditoraViewModel> EditoraExterior { get; set; } = [];
    public List<EditoraViewModel> EditoraBrasil { get; set; } = [];
    public List<StatusViewModel> Status { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var leitura = await client.GetFromJsonAsync<LeituraViewModel>(
            $"api/Leituras/{id}"
        );

        if (leitura == null)
        {
            return NotFound();
        }

        ItemId = leitura.ItemId;

        Leitura = new LeituraFormViewModel
        {
            Nome = leitura.Item.Nome,
            DataLancamento = leitura.Item.DataLancamento,
            EstadoId = leitura.Item.EstadoId,
            CodigoEAN = leitura.Item.CodigoEAN,
            DataAquisicao = leitura.Item.DataAquisicao,
            ValorAquisicao = leitura.Item.ValorAquisicao,
            FranquiaId = leitura.Item.FranquiaId,
            Observacoes = leitura.Item.Observacoes,
            Tipo = leitura.Tipo,
            EditoraExteriorId = leitura.EditoraExteriorId,
            EditoraBrasilId = leitura.EditoraBrasilId,
            Autor = leitura.Autor,
            StatusId = leitura.StatusId,
            Lingua = leitura.Lingua,
            ISBN13 = leitura.ISBN13,
            Volume = leitura.Volume,
            VolumeAte = leitura.VolumeAte
        };

        await CarregarListasAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.PutAsJsonAsync(
            $"api/Leituras/{ItemId}", Leitura);

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
            $"api/Leituras/{ItemId}"
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