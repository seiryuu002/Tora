using Microsoft.EntityFrameworkCore;
using Tora.Domain.Entities;

namespace Tora.Persistence;

public class ToraDbContext(DbContextOptions<ToraDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Domain.Entities.Task> Tasks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Role> Roles { get; set; }

}
