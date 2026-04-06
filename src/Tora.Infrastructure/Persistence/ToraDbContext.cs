using Microsoft.EntityFrameworkCore;
using Tora.Domain.Entities;

namespace Tora.Infrastructure;

public class ToraDbContext(DbContextOptions<ToraDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Domain.Entities.Task> Tasks => Set<Domain.Entities.Task>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Role> Roles => Set<Role>();

}
