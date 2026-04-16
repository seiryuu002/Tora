using Microsoft.EntityFrameworkCore;
using Tora.Application.Interfaces;
using Tora.Domain.Entities;

namespace Tora.Infrastructure.Persistence;

public class ToraDbContext(DbContextOptions<ToraDbContext> options) : DbContext(options), IToraDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Domain.Entities.Task> Tasks => Set<Domain.Entities.Task>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToraDbContext).Assembly);
    }

}
