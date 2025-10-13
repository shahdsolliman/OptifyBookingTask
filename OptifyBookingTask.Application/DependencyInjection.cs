using Microsoft.Extensions.DependencyInjection;
using OptifyBookingTask.Application.Abstracts.Services;
using OptifyBookingTask.Application.Mapping;
using OptifyBookingTask.Application.Services;
using System.Reflection;

namespace OptifyBookingTask.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<ITripService, TripService>();


            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


            return services;
        }
    }
}
