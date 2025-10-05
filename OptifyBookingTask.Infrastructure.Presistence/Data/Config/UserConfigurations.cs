using Microsoft.EntityFrameworkCore;
using OptifyBookingTask.Domain.Entities;

namespace OptifyBookingTask.Infrastructure.Presistence.Data.Config
{
    public class UserConfigurations : BaseEntityConfigurations<User, int>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.ToTable("Users");
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
