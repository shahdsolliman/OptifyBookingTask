using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OptifyBookingTask.Application.Abstracts.Models;

namespace OptifyBookingTask.UI.Pages.Trips.Reservations
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public ReservationToReturnDto Reservation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Reservation = await _httpClient.GetFromJsonAsync<ReservationToReturnDto>($"api/reservations/{id}")
                           ?? throw new Exception("Reservation not found");

            return Page();
        }
    }
}
