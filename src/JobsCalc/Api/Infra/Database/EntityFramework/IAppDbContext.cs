using JobsCalc.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobsCalc.Api.Infra.Database.EntityFramework;
public interface IAppDbContext
{
  public DbSet<User> Users { get; set; }
  public DbSet<Planning> Plannings { get; set; }
  public DbSet<Job> Jobs { get; set; }

  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}