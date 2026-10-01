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
    public int Id { get; set; }

    [BindProperty]
    public LeituraFormViewModel Leitura { get; set; } = new();

    public List<EstadoViewModel> Estados { get; set; } = [];
    
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

        Id = leitura.Id;

        Leitura = new LeituraFormViewModel
        {
            ColecaoLeituraId = leitura.ColecaoLeituraId,
            Titulo = leitura.Titulo,
            DataLancamento = leitura.DataLancamento,
            DataAquisicao = leitura.DataAquisicao,
            Status = leitura.Status,
            EstadoId = leitura.EstadoId,
            CodigoEAN = leitura.CodigoEAN,
            ISBN13 = leitura.ISBN13,
            Volume = leitura.Volume,
            ValorAquisicao = leitura.ValorAquisicao,
            Observacoes = leitura.Observacoes
        };

        await CarregarListasAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await CarregarListasAsync();
            return Page();
        }

        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.PutAsJsonAsync(
            $"api/Leituras/{Id}", Leitura);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(
                string.Empty,
                "Não foi possível salvar o volume. Verifique os dados.");

            await CarregarListasAsync();
            return Page();
        }

        return RedirectToPage(
            "Colecao",
            new { id = Leitura.ColecaoLeituraId });
    }

    public async Task<IActionResult> OnPostDeleteAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.DeleteAsync(
            $"api/Leituras/{Id}"
        );

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty,
                "Não foi possível excluir o volume. Verifique os dados.");

            await CarregarListasAsync();
            return Page();
        }

        return RedirectToPage("Colecao",new { id = Leitura.ColecaoLeituraId });
    }

    private async Task CarregarListasAsync()
    {

        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var todosEstados = await client
            .GetFromJsonAsync<List<EstadoViewModel>>(
            "api/Estados") ?? [];

        Estados = todosEstados
            .Where(e => e.TipoColecao == TipoColecao.Leitura)
            .ToList();
    }
}