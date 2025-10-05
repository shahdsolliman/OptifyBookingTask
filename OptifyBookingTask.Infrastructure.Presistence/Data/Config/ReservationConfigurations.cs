using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OptifyBookingTask.Domain.Entities;

namespace OptifyBookingTask.Infrastructure.Presistence.Data.Config
{
    public class ReservationConfigurations : BaseEntityConfigurations<Reservation, int>
    {
        public override void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.CustomerName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(r => r.Notes)
                   .HasMaxLength(500);

            builder.HasOne(r => r.User)
                   .WithMany()
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Trip)
                   .WithMany()
                   .HasForeignKey(r => r.TripId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
