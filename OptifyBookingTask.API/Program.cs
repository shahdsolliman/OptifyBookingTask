
using Microsoft.EntityFrameworkCore;
using OptifyBookingTask.API.Extensions;
using OptifyBookingTask.Domain.Contracts;
using OptifyBookingTask.Infrastructure.Presistence;
using OptifyBookingTask.Infrastructure.Presistence.Data;

namespace OptifyBookingTask.API
{
    public class Program
    {

        public static async Task Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services (DI)

            // Add services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add DbContext
            builder.Services.AddPresistenceServices(builder.Configuration);

            #endregion

            var app = builder.Build();

            #region Database Intialization

            await app.IntializeBookingContextAsync(); 

            #endregion

            #region Configure Kestrel Middlewares


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
