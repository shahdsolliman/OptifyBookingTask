using System.Text.Json;

namespace OptifyBookingTask.API.Controllers.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Deatails { get; set; }
        public ApiExceptionResponse(int statusCode, string? message = null, string? deatails = null)
            : base(statusCode, message)
        {
            Deatails = deatails;
        }

        public override string ToString()
        {
            var serialzerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Serialize(this, serialzerOptions);

        }
    }
}
