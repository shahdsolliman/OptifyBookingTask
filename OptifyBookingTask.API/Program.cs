using OptifyBookingTask.API.Extensions;
using OptifyBookingTask.Infrastructure.Presistence;
using OptifyBookingTask.Application;
using OptifyBookingTask.API.Controllers.Errors;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = false;
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
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// DbContext + Application Services
builder.Services.AddPresistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7013", "http://localhost:5041")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Initialize DB
await app.IntializeBookingContextAsync();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<OptifyBookingTask.API.Middlewares.ExceptionHandlerMiddleware>();
app.UseRouting();
app.UseCors();
app.UseAuthorization();
app.UseStatusCodePagesWithReExecute("/Errors/{0}");

app.MapControllers();

app.Run();
