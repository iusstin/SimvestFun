using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimvestFun.ApplicationCore.Entities;

namespace SimvestFun.Infrastructure
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(u => u.About)
                .HasMaxLength(300);

            builder.Property(u => u.Email)
                .HasMaxLength(100);

            builder.Property(u => u.About)
                .HasDefaultValue("");

            builder.Property(u => u.ForgotPasswordGuid)
                .HasDefaultValue(null);

        }
    }
}
