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

        public ReservationToReturnDto Reservation { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var reservation =
                await _httpClient.GetFromJsonAsync<ReservationToReturnDto>($"api/reservations/{id}");

            if (reservation == null)
            {
                return NotFound();
            }

            Reservation = reservation;
            return Page();
        }
    }
}
