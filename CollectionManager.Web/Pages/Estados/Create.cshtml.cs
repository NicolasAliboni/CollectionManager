using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using CollectionManager.Web.ViewModels;

namespace CollectionManager.Web.Pages.Estados;

public class CreateModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CreateModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public EstadoViewModel Estado { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _httpClientFactory.CreateClient("CollectionManagerApi");

        var response = await client.PostAsJsonAsync("api/Estados", Estado);

        if (!response.IsSuccessStatusCode)
        {
            return Page();
        }

        return RedirectToPage("Index");
    }
}