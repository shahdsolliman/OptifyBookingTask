using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OptifyBookingTask.Infrastructure.Presistence.Data;

namespace OptifyBookingTask.Infrastructure.Presistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookingDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("BookingContext"));
            });

            return services;
        }
    }
}
