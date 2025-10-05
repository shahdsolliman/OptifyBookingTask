using Microsoft.EntityFrameworkCore;
using OptifyBookingTask.Domain.Entities;

namespace OptifyBookingTask.Infrastructure.Presistence.Data
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext()
        {
            
        }
        public BookingDbContext(DbContextOptions<BookingDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all IEntityTypeConfiguration classes automatically
            // from the same assembly as AssemblyInformation
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
