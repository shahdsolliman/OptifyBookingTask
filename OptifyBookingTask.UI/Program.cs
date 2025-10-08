namespace OptifyBookingTask.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Razor Pages
            builder.Services.AddRazorPages();
            


            // HttpClient for API
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7141/"); // API URL
            });

            var app = builder.Build();
            //app.MapFallbackToPage("/Trips/Index");

            // Middlewares
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();


        }
    }
}
