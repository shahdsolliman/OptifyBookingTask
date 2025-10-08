using Microsoft.AspNetCore.Mvc;

namespace OptifyBookingTask.API.Common
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BaseApiController : ControllerBase
    {
    }
}
