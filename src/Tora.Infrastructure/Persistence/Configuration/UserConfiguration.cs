using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tora.Domain.Entities;

namespace Tora.Infrastructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(256);

        // Unique index
        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.Password).IsRequired().HasMaxLength(512);

        //Foreign key one user can have only one role
        builder.HasOne(u => u.Role)
        .WithMany()
        .HasForeignKey(u => u.RoleId)
        .OnDelete(DeleteBehavior.Restrict); // Don't cascade delete users if role is deleted

        // Index on role id 
        builder.HasIndex(u => u.RoleId);
    }
}
