using Microsoft.EntityFrameworkCore;
using OptifyBookingTask.Domain.Contracts;
using OptifyBookingTask.Domain.Entities;
using System.Text.Json;

namespace OptifyBookingTask.Infrastructure.Presistence.Data
{
    internal class BookingContextIntializer(BookingDbContext _dbContext) : IBookingContextIntializer
    {
        
        public async Task InitializeAsync()
        {
            var pendingMigrations = _dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
                await _dbContext.Database.MigrateAsync(); // Update-Database 
        }

        public async Task SeedAsync()
        {
            if (!_dbContext.Users.Any())
            {
                var users = await File.ReadAllTextAsync("../OptifyBookingTask.Infrastructure.Presistence/Data/Seeds/Users.json");
                var userList = JsonSerializer.Deserialize<List<User>>(users);

                if (userList?.Count > 0)
                {
                    await _dbContext.Set<User>().AddRangeAsync(userList);
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (!_dbContext.Trips.Any())
            {
                var trips = await File.ReadAllTextAsync("../OptifyBookingTask.Infrastructure.Presistence/Data/Seeds/Trips.json");
                var tripList = JsonSerializer.Deserialize<List<Trip>>(trips);

                if (tripList?.Count > 0)
                {
                    await _dbContext.Set<Trip>().AddRangeAsync(tripList);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
