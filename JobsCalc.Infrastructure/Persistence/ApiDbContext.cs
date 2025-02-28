using JobsCalc.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobsCalc.Infrastructure.Persistence;

public class ApiDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<PlanningEntity> Plannings { get; set; } = null!;
    public DbSet<JobEntity> Jobs { get; set; } = null!;
    
    public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}