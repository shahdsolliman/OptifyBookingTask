
using OptifyBookingTask.Infrastructure.Presistence;

namespace OptifyBookingTask.API
{
    public class Program
    {
        public static void Main(string[] args)
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


            #region Configure Middleware


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
