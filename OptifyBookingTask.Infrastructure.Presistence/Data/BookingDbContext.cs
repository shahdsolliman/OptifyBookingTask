using Microsoft.EntityFrameworkCore;
using OptifyBookingTask.Domain.Entities;

namespace OptifyBookingTask.Infrastructure.Presistence.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
