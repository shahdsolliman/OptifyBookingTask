using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OptifyBookingTask.Application.Abstracts.Models;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OptifyBookingTask.UI.Pages.Reservations
{
    public class UpdateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public UpdateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [BindProperty]
        public ReservationToReturnDto Reservation { get; set; } = new();

        public int TripId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7141/api/reservations/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var reservation = await response.Content.ReadFromJsonAsync<ReservationToReturnDto>();

            if (reservation == null)
                return NotFound();

            Reservation = reservation;

            // ?? ??? ?????? ??? TripId ?????? ????? ??? ??? ?????? ?? ???? ???????
            // ????? ??? DTO ?????? TripId
            TripId = await GetTripIdByTripName(reservation.TripName);

            return Page();
        }

        private async Task<int> GetTripIdByTripName(string tripName)
        {
            // ? ????? ??? DTO ?? ??? TripId? ???? TripId ?????? ?? ??? Trips API
            var tripsResponse = await _httpClient.GetAsync("https://localhost:7141/api/trips");
            if (!tripsResponse.IsSuccessStatusCode)
                return 1; // fallback value

            var trips = await tripsResponse.Content.ReadFromJsonAsync<List<TripDto>>();
            var trip = trips?.FirstOrDefault(t => t.Name == tripName);
            return trip?.Id ?? 1;
        }
    }

        public class TripDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }
}
