namespace OptifyBookingTask.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key)
            :base($"The {name} with id {key} is not found.")
        {
            
        }
    }
}
