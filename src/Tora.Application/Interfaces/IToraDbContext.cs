using Microsoft.EntityFrameworkCore;
using Tora.Domain.Entities;

namespace Tora.Application.Interfaces;

public interface IToraDbContext
{
    DbSet<User> Users {get;}
    DbSet<Role> Roles {get;}
    DbSet<RefreshToken> RefreshTokens {get;}
    DbSet<Project> Projects {get;}
    DbSet<Domain.Entities.Task> Tasks{get; }
    DbSet<Comment> Comments { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
