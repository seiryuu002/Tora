using Microsoft.EntityFrameworkCore;
using Tora.Domain.Entities;

namespace Tora.Application.Interfaces;

public interface IToraDbContext
{
    DbSet<User> Users {get; set;}
    DbSet<Role> Roles {get; set;}
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
