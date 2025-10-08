using Microsoft.AspNetCore.Mvc.RazorPages;
using OptifyBookingTask.Application.Abstracts.Models;

namespace OptifyBookingTask.UI.Pages.Reservations
{
    public class MyBookingsModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public List<ReservationToReturnDto> Reservations { get; set; } = new();

        public MyBookingsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7141/api/reservations");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<ReservationToReturnDto>>();
                Reservations = data ?? new List<ReservationToReturnDto>();
            }
        }
    }
}
