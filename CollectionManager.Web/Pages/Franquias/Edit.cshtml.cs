using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using CollectionManager.Web.ViewModels;

namespace CollectionManager.Web.Pages.Franquias;

public class EditModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EditModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public FranquiaViewModel Franquia { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var franquia = await client.GetFromJsonAsync<FranquiaViewModel>(
            $"api/Franquias/{id}"
        );

        if (franquia == null)
        {
            return NotFound();
        }

        Franquia = franquia;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.PutAsJsonAsync(
            $"api/Franquias/{Franquia.Id}",
            Franquia
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
            $"api/Franquias/{Franquia.Id}"
        );

        if (!response.IsSuccessStatusCode)
        {
            return Page();
        }

        return RedirectToPage("Index");
    }
}