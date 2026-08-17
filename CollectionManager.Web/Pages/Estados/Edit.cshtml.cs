using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using CollectionManager.Web.ViewModels;

namespace CollectionManager.Web.Pages.Estados;

public class EditModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EditModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public EstadoViewModel Estado { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var estado = await client.GetFromJsonAsync<EstadoViewModel>(
            $"api/Estados/{id}"
        );

        if (estado == null)
        {
            return NotFound();
        }

        Estado = estado;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.PutAsJsonAsync(
            $"api/Estados/{Estado.Id}",
            Estado
        );

        if (!response.IsSuccessStatusCode)
        {
            return Page();
        }

        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostDeleteAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.DeleteAsync(
            $"api/Estados/{Estado.Id}"
        );

        if (!response.IsSuccessStatusCode)
        {
            return Page();
        }

        return RedirectToPage("Index");
    }
}