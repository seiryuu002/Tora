using Microsoft.EntityFrameworkCore;
using Tora.Domain.Entities;

namespace Tora.Application.Interfaces;

public interface IToraDbContext
{
    DbSet<User> Users {get;}
    DbSet<Role> Roles {get;}
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
