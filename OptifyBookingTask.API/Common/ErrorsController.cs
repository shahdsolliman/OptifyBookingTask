using Microsoft.AspNetCore.Mvc;
using OptifyBookingTask.API.Controllers.Errors;
using System.Net;

namespace OptifyBookingTask.API.Common
{
    [ApiController]
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error(int code)
        {
            if (code == (int)HttpStatusCode.NotFound)
            {
                var response = new ApiResponse((int)HttpStatusCode.NotFound,
                    $"The requested endpoint '{Request.Path}' was not found.");
                return NotFound(response);
            }

            var genericResponse = new ApiResponse(code, "An error occurred.");
            return new ObjectResult(genericResponse) { StatusCode = code };
        }
    }
}
