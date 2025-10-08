using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OptifyBookingTask.Application.Abstracts.Models;
using System.Net.Http.Json;

namespace OptifyBookingTask.UI.Pages.Trips
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public TripDto? Trip { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            Trip = await client.GetFromJsonAsync<TripDto>($"api/trips/{id}");

            if (Trip == null)
                return NotFound();

            return Page();
        }
    }
}
