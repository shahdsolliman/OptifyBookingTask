using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OptifyBookingTask.UI.Pages.Feedbacks
{
    public class IndexModel : PageModel
    {
        public List<FeedbackDto> Feedbacks { get; set; } = new();

        public void OnGet()
        {
            // ?????? ?????? Dummy
            Feedbacks = new List<FeedbackDto>
            {
                new FeedbackDto { UserName = "Ali", Message = "Great experience!", Date = DateTime.Now.AddDays(-2) },
                new FeedbackDto { UserName = "Sara", Message = "Loved the Nile cruise.", Date = DateTime.Now.AddDays(-1) },
            };
        }
    }

    public class FeedbackDto
    {
        public string UserName { get; set; } = "";
        public string Message { get; set; } = "";
        public DateTime Date { get; set; }
    }
}
