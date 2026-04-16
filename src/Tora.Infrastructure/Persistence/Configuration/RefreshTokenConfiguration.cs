using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tora.Domain.Entities;

namespace Tora.Infrastructure.Persistence.Configuration;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Token)
        .IsRequired()
        .HasMaxLength(512);

        builder.Property(r => r.TokenPrefix)
        .IsRequired()
        .HasMaxLength(8);

        builder.HasIndex(u => u.TokenPrefix);
        builder.HasIndex(u => u.UserId);

        builder.HasOne(r => r.User)
        .WithMany()
        .HasForeignKey(r => r.UserId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
