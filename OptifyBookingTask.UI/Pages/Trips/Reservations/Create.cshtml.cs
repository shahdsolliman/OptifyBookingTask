using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OptifyBookingTask.Application.Abstracts.Models;

namespace OptifyBookingTask.UI.Pages.Trips.Reservations
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        [BindProperty]
        public ReservationCreateDto Reservation { get; set; } = new();

        public TripDto Trip { get; set; } = default!;

        public async Task OnGetAsync(int tripId)
        {
            Trip = await _httpClient.GetFromJsonAsync<TripDto>($"api/trips/{tripId}")
                   ?? throw new Exception("Trip not found");

            Reservation.TripId = tripId;
            Reservation.ReservationDate = DateTime.Today;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var response = await _httpClient.PostAsJsonAsync("api/reservations", Reservation);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/MyBookings");
            }

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, error);
            return Page();
        }
    }
}
