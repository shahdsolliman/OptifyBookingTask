using OptifyBookingTask.Domain.Contracts;

namespace OptifyBookingTask.API.Extensions
{
    public static class IntializerExtensions
    {
        public static async Task<WebApplication> IntializeBookingContextAsync(this WebApplication app)
        {

            #region Update Database And Seed Data

            // Apply migrations at startup

            using var scope = app.Services.CreateAsyncScope();    // using for IDisposable 
            var services = scope.ServiceProvider;
            var bookingContextIntializer = services.GetRequiredService<IBookingContextIntializer>();
            // Ask Runtime Env for an object from "BookingContext" service exblicitly

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await bookingContextIntializer.InitializeAsync();
                await bookingContextIntializer.SeedAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                // handle exceptions that may occur during migration to avoid crashing the app
                // Log errors or handle them as needed
                logger.LogError(ex, "An error occurred while migrating the database or seeding data.");
            }

            #endregion

            return app;
        }
    }
}
