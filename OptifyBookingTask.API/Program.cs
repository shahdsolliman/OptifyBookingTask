using OptifyBookingTask.API.Extensions;
using OptifyBookingTask.Infrastructure.Presistence;
using OptifyBookingTask.Application;

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

            // Swagger + XML Comments
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            // Add DbContext
            builder.Services.AddPresistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();

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
