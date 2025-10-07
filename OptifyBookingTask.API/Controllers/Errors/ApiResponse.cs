using System.Text.Json;

namespace OptifyBookingTask.API.Controllers.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int statusCode, string? message)
        {
            StatusCode = statusCode;
            Message = message ??  GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            
            return statusCode switch
            {
                400 => "A bad requestm you have made",
                401 => "Authorized, you are not",
                404 => "Resource wa not found",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leeds to career change.",
                _ => null
            };
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
