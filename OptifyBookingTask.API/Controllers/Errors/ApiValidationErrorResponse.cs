﻿namespace OptifyBookingTask.API.Controllers.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public required IEnumerable<ValidationError> Errors { get; set; }
        public ApiValidationErrorResponse(string? message)
            : base(400, message)
        {
        }

        public class ValidationError
        {
            public required string Field { get; set; }
            public required IEnumerable<string> Error { get; set; }
        }
    }
}
