using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimvestFun.ApplicationCore.Entities;

namespace SimvestFun.Infrastructure
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.HasKey(s => s.Key);

            builder.Property(s => s.Value)
                .HasDefaultValue("");
        }
    }
}
