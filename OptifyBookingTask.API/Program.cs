using OptifyBookingTask.API.Extensions;
using OptifyBookingTask.Infrastructure.Presistence;
using OptifyBookingTask.Application;
using OptifyBookingTask.API.Controllers.Errors;
using Microsoft.AspNetCore.Mvc;

namespace OptifyBookingTask.API
{
    public class Program
    {

        public static async Task Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services (DI)

            // Add services
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = false;   // Will Not Excute any endpoint if ModelState is Invalid
                    options.InvalidModelStateResponseFactory = (actionContext) =>
                    {
                        var errors = actionContext.ModelState
                            .Where(e => e.Value?.Errors.Count > 0)
                            .Select(e => new ApiValidationErrorResponse.ValidationError()
                            {
                                Field = e.Key,
                                Error = e.Value!.Errors.Select(x => x.ErrorMessage)
                            }).ToArray();
                        return new BadRequestObjectResult(new ApiValidationErrorResponse("Validation errors occurred")
                        {
                            Errors = errors
                        });
                    };

                });

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

            app.UseRouting();


            app.UseAuthorization();

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");


            #endregion

            app.Run();
        }
    }
}
