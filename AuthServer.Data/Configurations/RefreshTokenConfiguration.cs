using AuthServer.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServer.Data.Configurations;
public class RefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.Property(x=> x.RefreshToken).IsRequired().HasMaxLength(200);
    }

}
