using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using OptifyBookingTask.Application.Abstracts.Models;

namespace OptifyBookingTask.UI.Pages.Trips
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public List<TripDto> Trips { get; set; } = new();

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<TripDto>>("api/trips");
            if (response != null)
                Trips = response;
        }
    }
}
