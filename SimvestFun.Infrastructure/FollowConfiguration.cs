using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimvestFun.ApplicationCore.Entities;

namespace SimvestFun.Infrastructure
{
    public class FollowConfiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.HasKey(f => new { f.UserId, f.FollowedUserId });
        }
    }
}
