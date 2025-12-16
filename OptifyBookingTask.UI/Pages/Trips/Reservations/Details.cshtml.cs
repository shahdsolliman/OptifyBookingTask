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

        public async Task<IActionResult> OnGetAsync(int id, string? tripName, string? cityName)
        {
            var reservation =
                await _httpClient.GetFromJsonAsync<ReservationToReturnDto>($"api/reservations/{id}");

            if (reservation == null)
            {
                return NotFound();
            }

            // If backend did not populate TripName/CityName, fall back to values passed from the create flow (if any)
            if (!string.IsNullOrWhiteSpace(tripName) && string.IsNullOrWhiteSpace(reservation.TripName))
            {
                reservation.TripName = tripName;
            }

            if (!string.IsNullOrWhiteSpace(cityName) && string.IsNullOrWhiteSpace(reservation.CityName))
            {
                reservation.CityName = cityName;
            }

            Reservation = reservation;
            return Page();
        }
    }
}
