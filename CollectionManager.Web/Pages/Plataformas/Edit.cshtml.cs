using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using CollectionManager.Web.ViewModels;

namespace CollectionManager.Web.Pages.Plataformas;

public class EditModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EditModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public PlataformaViewModel Plataforma { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var plataforma = await client.GetFromJsonAsync<PlataformaViewModel>(
            $"api/Plataformas/{id}"
        );

        if (plataforma == null)
        {
            return NotFound();
        }

        Plataforma = plataforma;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.PutAsJsonAsync(
            $"api/Plataformas/{Plataforma.Id}",
            Plataforma
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
            $"api/Plataformas/{Plataforma.Id}"
        );

        if (!response.IsSuccessStatusCode)
        {
            return Page();
        }

        return RedirectToPage("Index");
    }
}