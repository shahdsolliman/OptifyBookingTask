using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OptifyBookingTask.Domain.Entities;

namespace OptifyBookingTask.Infrastructure.Presistence.Data.Config
{
    public class TripConfigurations : BaseEntityConfigurations<Trip, int>
    {
        public override void Configure(EntityTypeBuilder<Trip> builder)
        {
            base.Configure(builder);

            builder.ToTable("Trips");

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.CityName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Price)
                .HasColumnType("decimal(10,2)");

            builder.Property(t => t.ImageUrl)
                .HasMaxLength(500);

            builder.Property(t => t.Content)
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.CreationDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
